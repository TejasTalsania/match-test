using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

// UI event listener and react to events to update ui during gameplay...
public class GameplayUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTurnCount;
    [SerializeField] private TextMeshProUGUI textMatchCount;
    [SerializeField] private TextMeshProUGUI textLevelNumber;

    private int _turnCount;
    private int _matchCount;
    private int _comboCount;

    private void OnEnable()
    {
        GameEvents.OnResetLevel += ResetLevelData;
        GameEvents.OnTurnUpdated += UpdateTurnCount;
        GameEvents.OnMatchCardUpdated += UpdateMatchCount;
        GameEvents.OnLevelNumberUpdated += UpdateLevelnumber;
        GameEvents.OnLevelResumed += UpdateDataFoeResumedLevel;
    }

    private void OnDisable()
    {
        GameEvents.OnResetLevel -= ResetLevelData;
        GameEvents.OnTurnUpdated -= UpdateTurnCount;
        GameEvents.OnMatchCardUpdated -= UpdateMatchCount;
        GameEvents.OnLevelNumberUpdated -= UpdateLevelnumber;
        GameEvents.OnLevelResumed -= UpdateDataFoeResumedLevel;
    }

    private void ResetLevelData()
    {
        _comboCount = 0;
        _turnCount = 0;
        textTurnCount.text = _turnCount.ToString();
        _matchCount = 0;
        textMatchCount.text = _matchCount.ToString();
    }

    private void UpdateTurnCount()
    {
        _turnCount++;
        textTurnCount.text = _turnCount.ToString();
        SaveManager.UpdateTurnCount(_turnCount);
    }

    private void UpdateMatchCount()
    {
        _matchCount++;
        textMatchCount.text = _matchCount.ToString();
        SaveManager.UpdateMatchCount(_matchCount);
    }

    private void UpdateLevelnumber(int number)
    {
        textLevelNumber.text = number.ToString();
    }
    
    private void UpdateDataFoeResumedLevel(LevelSaveData obj)
    {
        UpdateLevelnumber(obj.levelNumber);
        _comboCount = obj.comboCount;
        _turnCount = obj.turnCount;
        textTurnCount.text = _turnCount.ToString();
        _matchCount = obj.matchCount;
        textMatchCount.text = _matchCount.ToString();
    }
}
