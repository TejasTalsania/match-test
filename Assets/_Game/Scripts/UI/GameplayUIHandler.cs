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

    private void OnEnable()
    {
        ResetTurnAndMatchCount();
        GameEvents.OnTurnTaken += UpdateTurnCount;
        GameEvents.OnMatchSuccess += UpdateMatchCount;
        GameEvents.OnLevelNumberUpdated += UpdateLevelnumber;
    }

    private void OnDisable()
    {
        GameEvents.OnTurnTaken -= UpdateTurnCount;
        GameEvents.OnMatchSuccess -= UpdateMatchCount;
        GameEvents.OnLevelNumberUpdated -= UpdateLevelnumber;
    }

    private void ResetTurnAndMatchCount()
    {
        _turnCount = 0;
        textTurnCount.text = _turnCount.ToString();
        _matchCount = 0;
        textMatchCount.text = _matchCount.ToString();
    }

    private void UpdateTurnCount()
    {
        _turnCount++;
        textTurnCount.text = _turnCount.ToString();
    }

    private void UpdateMatchCount()
    {
        _matchCount++;
        textMatchCount.text = _matchCount.ToString();
    }

    private void UpdateLevelnumber(int number)
    {
        textLevelNumber.text = number.ToString();
    }
}
