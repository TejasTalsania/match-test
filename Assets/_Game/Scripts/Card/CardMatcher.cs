// Card matching responsibility...

using System;
using UnityEngine;

public class CardMatcher
{
    private CardView _firstCard;
    private CardView _secondCard;

    public event Action<bool> OsTurnCompleted;

    public void SelectCard(CardView card)
    {
        // if card is matched then return...
        if (card.IsMatched())
        {
            return;
        }

        // if first and second card both not null then both cards are not matched so set as unmatched and hide...
        if (_firstCard != null && _secondCard != null)
        {
            ResetUnmatched();
        }

        // if first card is null then assign it...
        if (_firstCard == null)
        {
            _firstCard = card;
            return;
        }
        
        // if second card is null and not same as first card then assign second card and complete turn and check for match...
        if (_secondCard == null && card != _firstCard)
        {
            _secondCard = card;
            CompleteTurn();
        }
    }

    // hide cards again if not matched...
    private void ResetUnmatched()
    {
        if (!_firstCard.IsMatched()) _firstCard.HideFrontSide();
        if (!_secondCard.IsMatched()) _secondCard.HideFrontSide();

        _firstCard = null;
        _secondCard = null;
    }
    
    private void CompleteTurn()
    {
        var isMatch = _firstCard.GetId() ==  _secondCard.GetId();
        if (isMatch)
        {
            GameEvents.FireSoundRequested(SoundType.CardMatch);
            _firstCard.SetMatched();
            _secondCard.SetMatched();
        }
        else
        {
            GameEvents.FireSoundRequested(SoundType.CardNoMatch);
        }
        
        OsTurnCompleted?.Invoke(isMatch);
    }
}