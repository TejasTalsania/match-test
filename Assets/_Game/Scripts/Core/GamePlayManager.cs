using System;
using UnityEngine;
using System.Collections.Generic;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private GridView gridView;
    [SerializeField] private CardFrontSpritesData cardFrontSprites;

    private CardMatcher matcher;
    private List<CardView> activeCards = new();

    private int _currentLevel = 1;
    private int _comboCount = 0;

    private void OnEnable()
    {
        matcher = new CardMatcher();
        matcher.OsTurnCompleted += HandleTurnResult;
        GameEvents.OnPlayButtonPressed += HandlePlayButtonPress;
        GameEvents.OnResumeButtonPressed += HandleResumeButtonPress;
    }

    private void OnDisable()
    {
        matcher.OsTurnCompleted -= HandleTurnResult;
        GameEvents.OnPlayButtonPressed -= HandlePlayButtonPress;
        GameEvents.OnResumeButtonPressed -= HandleResumeButtonPress;
    }

    private void HandlePlayButtonPress()
    {
        _currentLevel = PlayerPrefs.GetInt(GameConfig.CurrentLevelPrefName, 1);
        if (_currentLevel > GameConfig.TotalLevels)
            _currentLevel = 1;
        GameEvents.FireLevelNumberUpdated(_currentLevel); // level number update handle...
        LoadLevel(_currentLevel);
    }
    
    // Loads level from json and generate cards to play...
    private void LoadLevel(int level)
    {
        // loads level data from json loader...
        var data = JsonLoader.LoadLevel(level);

        if (data == null)
        {
            Debug.LogError("Level number " +  level + " not found");
            return;
        }
        
        // Reset level and destroy all generated cards...
        GameEvents.FireResetLevel();
        gridView.ResetLevel();
        gridView.SetRowColumn(data.columns);

        // generate card id pairs from level data
        var ids = GenerateCardPairs(data.cardIds);
        
        // shuffle all cards for random place
        ShuffleUtility.Shuffle(ids);

        List<CardModel> models = new();
        for (var i = 0; i < ids.Count; i++)
        {
            var id = ids[i];
            models.Add(new CardModel(id));
        }

        // instantiate cards from level data
        activeCards = gridView.SpawnCards(models);

        // set cards data as per level
        for (var i = 0; i < activeCards.Count; i++)
        {
            var id = ids[i];
            var frontSprite = cardFrontSprites.GetSprite(id);
            activeCards[i].Initialize(models[i], frontSprite);
            activeCards[i].OnClicked += OnCardClicked;
        }
        
        // save data from start if user quits between level...
        SaveManager.SetDataOnLevelStart(_currentLevel, data.rows, data.columns, activeCards);
    }
    
    private void HandleResumeButtonPress()
    {
        // loads data from player prefs for half completed level quits...
        var dataStr = PlayerPrefs.GetString(GameConfig.CurrentLevelDataPrefName, string.Empty);
        var savedData = JsonUtility.FromJson<LevelSaveData>(dataStr);
        
        // Reset level and destroy all generated cards...
        gridView.ResetLevel();
        gridView.SetRowColumn(savedData.columns);
        
        // generate card id pairs from level data
        var cardIds = new int[savedData.activeCards.Count];
        for (var i = 0; i < savedData.activeCards.Count; i++)
        {
            cardIds[i] = savedData.activeCards[i].id;
        }
        
        List<CardModel> models = new();
        for (var i = 0; i < cardIds.Length; i++)
        {
            var id = cardIds[i];
            models.Add(new CardModel(id));
        }

        // instantiate cards from level data
        activeCards = gridView.SpawnCards(models);

        // set cards data as per level
        for (var i = 0; i < activeCards.Count; i++)
        {
            var id = cardIds[i];
            var frontSprite = cardFrontSprites.GetSprite(id);
            activeCards[i].Initialize(models[i], frontSprite);
            
            // if previously copleted level then hide that cards...
            if (savedData.activeCards[i].isMatched)
            {
                activeCards[i].SetCardFrontSprite();
                activeCards[i].SetMatched();
            }
            else
            {
                activeCards[i].OnClicked += OnCardClicked;
            }
        }
        
        GameEvents.FireLevelResumed(savedData);
        SaveManager.SetOldDataOnLevelResumed();
    }

    private void HandleTurnResult(bool result)
    {
        GameEvents.FireTurnTaken();
        if (result)
        {
            _comboCount++;
            GameEvents.FireScoreUpdate(GameConfig.MatchScore);
            GameEvents.FireMatchSuccess();
            CheckLevelComplete();
        }
        else
        {
            _comboCount = 0;
        }

        // if combo increases then score increase according to that...
        if (_comboCount > 1)
        {
            var comboCount = _comboCount - 1;
            GameEvents.FireComboUpdate(comboCount); // here -1 because 1st match is not count as a combo...
            if (comboCount > 0)
            {
                var scoreToAdd = comboCount * GameConfig.ComboScore;
                GameEvents.FireScoreUpdate(scoreToAdd);
            }
        }
    }

    // when card clicked then try to match here and if matched check for level complete...
    private void OnCardClicked(CardView card)
    {
        matcher.SelectCard(card);
    }
    
    // Level compelte logic...
    private void CheckLevelComplete()
    {
        var isCompleted = true;
        // if all cards are marked as matched then level completed...
        for (var i = 0; i < activeCards.Count; i++)
        {
            var cardView = activeCards[i].GetComponent<CardView>();
            if (!cardView.IsMatched())
            {
                isCompleted = false;
                break;
            }
        }

        if (isCompleted)
        {
            Invoke(nameof(ShowLevelCompleteScreen), 0.6F);
        }
        else
        {
            SaveManager.UpdateCardStatus(activeCards);
        }
    }

    private void ShowLevelCompleteScreen()
    {
        SaveManager.ResetDataOnLevelComplete();
        GameEvents.FireSoundRequested(SoundType.LevelComplete);
        GameEvents.FireLevelCompleted();
    }

    // generate card pairs from level json to play fair...
    private List<int> GenerateCardPairs(int[] ids)
    {
        List<int> result = new();
        foreach (var id in ids)
        {
            result.Add(id);
            result.Add(id);
        }
        return result;
    }
}