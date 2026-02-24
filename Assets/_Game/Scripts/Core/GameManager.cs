using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
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
    }

    private void OnDisable()
    {
        matcher.OsTurnCompleted -= HandleTurnResult;
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

    private void Start()
    {
        LoadLevel(currentLevel);
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
            Debug.Log("TEST 1 = " + "Level Completed = " + currentLevel);
        }
        else
        {
            Debug.Log("TEST 2 = " + "Level Not Completed = " + currentLevel);
        }
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