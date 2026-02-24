using System.Collections.Generic;
using UnityEngine;

public class SaveManager
{
    public static LevelSaveData levelSaveData;

    public static void SetDataOnLevelStart(int levelNumber, int rows, int columns, List<CardView> cards)
    {
        levelSaveData =  new LevelSaveData();
        levelSaveData.levelNumber = levelNumber;
        levelSaveData.turnCount = 0;
        levelSaveData.matchCount = 0;
        levelSaveData.comboCount = 0;
        levelSaveData.score = 0;
        levelSaveData.rows = rows;
        levelSaveData.columns = columns;
        levelSaveData.activeCards = new List<CardStatus>();

        for (var i = 0; i < cards.Count; i++)
        {
            var cardStatus = new CardStatus();
            cardStatus.isMatched = false;
            cardStatus.id = cards[i].GetId();
            levelSaveData.activeCards.Add(cardStatus);
        }

        SaveData();
    }

    public static void SetOldDataOnLevelResumed()
    {
        levelSaveData =  new LevelSaveData();
        var dataStr = PlayerPrefs.GetString(GameConfig.CurrentLevelDataPrefName, string.Empty);
        levelSaveData = JsonUtility.FromJson<LevelSaveData>(dataStr);
    }

    private static void SaveData()
    {
        var str =  JsonUtility.ToJson(levelSaveData);
        PlayerPrefs.SetString(GameConfig.CurrentLevelDataPrefName, str);
    }

    public static void ResetDataOnLevelComplete()
    {
        PlayerPrefs.SetString(GameConfig.CurrentLevelDataPrefName, string.Empty);
    }

    public static void UpdateTurnCount(int turnCount)
    {
        levelSaveData.turnCount = turnCount;
        SaveData();
    }
    
    public static void UpdateMatchCount(int matchCount)
    {
        levelSaveData.matchCount = matchCount;
        SaveData();
    }
    
    public static void UpdateComboCount(int comboCount)
    {
        levelSaveData.comboCount = comboCount;
        SaveData();
    }
    
    public static void UpdateScore(int score)
    {
        levelSaveData.score = score;
        SaveData();
    }

    public static void UpdateCardStatus(List<CardView> cards)
    {
        for (var i = 0; i < cards.Count; i++)
        {
            levelSaveData.activeCards[i].isMatched = cards[i].IsMatched();
        }
        
        SaveData();
    }
}

[System.Serializable]
public class LevelSaveData
{
    public int levelNumber;
    public int turnCount;
    public int matchCount;
    public int comboCount;
    public int score;
    public int rows;
    public int columns;
    public List<CardStatus> activeCards =  new List<CardStatus>();
}

[System.Serializable]
public class CardStatus
{
    public int id;
    public bool isMatched;
}