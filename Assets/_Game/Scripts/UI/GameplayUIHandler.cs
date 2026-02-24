using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

// UI event listener and react to events to update ui during gameplay...
public class GameplayUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTurnCount;
    [SerializeField] private TextMeshProUGUI textMatchCount;
    [SerializeField] private TextMeshProUGUI textLevelNumber;
    [SerializeField] private TextMeshProUGUI textComboCount;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textHighScore;

    private int _turnCount;
    private int _matchCount;
    private int _score;

    private void OnEnable()
    {
        GameEvents.OnResetLevel += ResetLevelData;
        GameEvents.OnTurnUpdated += UpdateTurnCount;
        GameEvents.OnMatchCardUpdated += UpdateMatchCount;
        GameEvents.OnComboUpdated += UpdateComboCount;
        GameEvents.OnScoreUpdated += UpdateScore;
        GameEvents.OnLevelNumberUpdated += UpdateLevelnumber;
        GameEvents.OnLevelResumed += UpdateDataFoeResumedLevel;
        GameEvents.OnLevelCompleted += UpdateHighScore;
    }

    private void OnDisable()
    {
        GameEvents.OnResetLevel -= ResetLevelData;
        GameEvents.OnTurnUpdated -= UpdateTurnCount;
        GameEvents.OnMatchCardUpdated -= UpdateMatchCount;
        GameEvents.OnComboUpdated -= UpdateComboCount;
        GameEvents.OnScoreUpdated -= UpdateScore;
        GameEvents.OnLevelNumberUpdated -= UpdateLevelnumber;
        GameEvents.OnLevelResumed -= UpdateDataFoeResumedLevel;
        GameEvents.OnLevelCompleted -= UpdateHighScore;
    }

    private void ResetLevelData()
    {
        _turnCount = 0;
        textTurnCount.text = "Turn : " + _turnCount.ToString();
        _matchCount = 0;
        textMatchCount.text = "Match : " + _matchCount.ToString();
        textComboCount.text = "Combo : 0";
        _score = 0;
        textScore.text = "Score : " + _score.ToString();
        UpdateHighScore();
    }

    private void UpdateTurnCount()
    {
        _turnCount++;
        textTurnCount.text = "Turn : " + _turnCount.ToString();
        SaveManager.UpdateTurnCount(_turnCount);
    }

    private void UpdateMatchCount()
    {
        _matchCount++;
        textMatchCount.text = "Match : " + _matchCount.ToString();
        SaveManager.UpdateMatchCount(_matchCount);
    }

    private void UpdateLevelnumber(int number)
    {
        textLevelNumber.text = "Level : " + number.ToString();
    }
    
    private void UpdateComboCount(int number)
    {
        textComboCount.text = "Combo : " + number.ToString();
        SaveManager.UpdateComboCount(number);
    }
    
    private void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        textScore.text = "Score : " + _score.ToString();
        PlayerPrefs.SetInt(GameConfig.CurrentScorePrefName, _score);
        SaveManager.UpdateScore(_score);
        UpdateHighScore();
    }
    
    private void UpdateDataFoeResumedLevel(LevelSaveData obj)
    {
        UpdateLevelnumber(obj.levelNumber);
        _turnCount = obj.turnCount;
        textTurnCount.text = "Turn : " + _turnCount.ToString();
        _matchCount = obj.matchCount;
        textMatchCount.text = "Match : " + _matchCount.ToString();
        textComboCount.text = "Combo : " + obj.comboCount.ToString();
        _score = obj.score;
        textScore.text = "Score : " + obj.score.ToString();
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        var lastHighScore = PlayerPrefs.GetInt(GameConfig.HighScorePrefName, 0);
        if (_score > lastHighScore)
        {
            lastHighScore = _score;
            PlayerPrefs.SetInt(GameConfig.HighScorePrefName, lastHighScore);
        }

        SetHighScoreText(lastHighScore);
    }

    private void SetHighScoreText(int score)
    {
        textHighScore.text = "High Score : " + score.ToString();
    }
}
