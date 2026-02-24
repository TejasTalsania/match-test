using TMPro;
using UnityEngine;

// UI event listener and react to events to update ui during gameplay...
public class GameplayUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private TextMeshProUGUI matchText;

    private int _turnCount;
    private int _matchCount;

    private void OnEnable()
    {
        ResetTurnAndMatchCount();
        GameEvents.OnTurnTaken += UpdateTurnCount;
        GameEvents.OnMatchSuccess += UpdateMatchCount;
    }

    private void OnDisable()
    {
        GameEvents.OnTurnTaken -= UpdateTurnCount;
        GameEvents.OnMatchSuccess -= UpdateMatchCount;
    }

    private void ResetTurnAndMatchCount()
    {
        _turnCount = 0;
        turnText.text = _turnCount.ToString();
        _matchCount = 0;
        matchText.text = _matchCount.ToString();
    }

    private void UpdateTurnCount()
    {
        _turnCount++;
        turnText.text = _turnCount.ToString();
    }

    private void UpdateMatchCount()
    {
        _matchCount++;
        matchText.text = _matchCount.ToString();
    }
}
