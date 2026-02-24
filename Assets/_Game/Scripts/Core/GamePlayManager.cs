using System;
using UnityEngine;
using System.Collections.Generic;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private GridView gridView;
    [SerializeField] private CardFrontSpritesData cardFrontSprites;

    private CardMatcher matcher;
    private List<CardView> activeCards = new();

    private int currentLevel = 1;

    private void OnEnable()
    {
        matcher = new CardMatcher();
        matcher.OsTurnCompleted += HandleTurnResult;
        GameEvents.OnPlayButtonPressed += HandlePlayButtonPress;
    }

    private void OnDisable()
    {
        matcher.OsTurnCompleted -= HandleTurnResult;
        GameEvents.OnPlayButtonPressed -= HandlePlayButtonPress;
    }

    private void HandlePlayButtonPress()
    {
        currentLevel = PlayerPrefs.GetInt(GameConfig.CurrentLevelPrefName, 1);
        if (currentLevel > GameConfig.TotalLevels)
            currentLevel = 1;
        GameEvents.OnLevelNumberUpdated(currentLevel); // level number update handle...
        LoadLevel(currentLevel);
    }

    private void HandleTurnResult(bool result)
    {
        GameEvents.FireTurnTaken();
        if (result)
        {
            GameEvents.FireMatchSuccess();
            CheckLevelComplete();
        }
    }

    // Loads level from json and generate cards to play...
    private void LoadLevel(int level)
    {
        var data = JsonLoader.LoadLevel(level);
        
        gridView.ResetLevel();
        gridView.SetRowColumn(data.columns);

        var ids = GenerateCardPairs(data.cardIds);
        ShuffleUtility.Shuffle(ids);

        List<CardModel> models = new();
        for (var i = 0; i < ids.Count; i++)
        {
            var id = ids[i];
            models.Add(new CardModel(id));
        }

        activeCards = gridView.SpawnCards(models);

        for (var i = 0; i < activeCards.Count; i++)
        {
            var id = ids[i];
            var frontSprite = cardFrontSprites.GetSprite(id);
            activeCards[i].Initialize(models[i], frontSprite);
            activeCards[i].OnClicked += OnCardClicked;
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
            Invoke(nameof(ShowLevelCompleteScreen), 0.5F);
        }
    }

    private void ShowLevelCompleteScreen()
    {
        ScreenManager.Instance.ShowScreen(ScreenType.LevelCompleteScreen);
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