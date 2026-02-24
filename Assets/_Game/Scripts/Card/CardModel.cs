public class CardModel
{
    public int Id { get; private set; }
    public bool IsMatched { get; private set; }
    private bool _isOpened = false;

    public CardModel(int id)
    {
        Id = id;
    }

    public void SetMatched()
    {
        IsMatched = true;
    }

    public bool IsCardMatched()
    {
        return IsMatched;
    }
    
    public void SetOpened()
    {
        _isOpened = true;
    }

    public void SetClosed()
    {
        _isOpened = false;
    }

    public bool IsCardOpened()
    {
        return _isOpened;
    }
}