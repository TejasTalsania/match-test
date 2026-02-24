// Card matching responsibility...
public class CardMatcher
{
    private CardView _firstCard;
    private CardView _secondCard;
    
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