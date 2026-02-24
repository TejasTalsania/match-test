// Card matching responsibility...

using System;

public class CardMatcher
{
    private CardView _firstCard;
    private CardView _secondCard;

    public event Action<bool> OsTurnCompleted;

    public void SelectCard(CardView card)
    {
        if (card.IsMatched())
        {
            return;
        }

        if (_firstCard != null && _secondCard != null)
        {
            ResetUnmatched();
        }

        if (_firstCard == null)
        {
            _firstCard = card;
            return;
        }

        if (_secondCard == null && card != _firstCard)
        {
            _secondCard = card;
            CompleteTurn();
        }
    }

    public void ResetUnmatched()
    {
        if (!_firstCard.IsMatched()) _firstCard.HideFrontSide();
        if (!_secondCard.IsMatched()) _secondCard.HideFrontSide();

        _firstCard = null;
        _secondCard = null;
    }
    
    public void CompleteTurn()
    {
        var isMatch = _firstCard.GetId() ==  _secondCard.GetId();
        if (isMatch)
        {
            _firstCard.SetMatched();
            _secondCard.SetMatched();
        }
        
        OsTurnCompleted?.Invoke(isMatch);
    }
    
    
    // Match for 2 opened cards...
    public bool TryMatch(CardView card)
    {
        // if first card is opened then assign first card and return from here as second card is not opened yet...
        if (_firstCard == null)
        {
            _firstCard = card;
            return false;
        }

        // if first card is opened then assign second card...
        _secondCard = card;

        // if first card is and second card is same no need to assign it as a second card...
        if (_firstCard == _secondCard)
        {
            _secondCard = null;
            return false;
        }

        // if both cards not same then return false...
        if (_firstCard.GetId() != _secondCard.GetId()) return false;
        
        // if both cards are same then mark as a matched and reset first and second cards for other cards...
        _firstCard.SetMatched();
        _secondCard.SetMatched();
        Reset();
        return true;
    }

    private void Reset()
    {
        _firstCard = null;
        _secondCard = null;
    }

    // if both cards not matched then hide both card again from game manager...
    public void HideCards()
    {
        _firstCard?.HideFrontSide();
        _secondCard?.HideFrontSide();
        Reset();
    }
    
    public bool IsSecondCard()
    {
        return _secondCard != null;
    }
}