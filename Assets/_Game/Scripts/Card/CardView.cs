using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

// Only card ui related responsibility 
public class CardView : MonoBehaviour
{
    [SerializeField] private Image cardSprite;
    private Sprite _frontSprite, _backSprite;

    private CardModel _model;
    public Action<CardView> OnClicked;

    // initialize card when generated with basic details...
    public void Initialize(CardModel model, Sprite frontImage)
    {
        _model = model;
        _frontSprite = frontImage;
        _backSprite = cardSprite.sprite;
        // SetCardBackSprite();
    }

    // Card flip animation to show front side...
    private void ShowFrontSide()
    {
        transform.DOLocalRotate(new Vector3(0, 90, 0), 0.05F).OnComplete(() =>
        {
            SetCardFrontSprite();
            transform.DOLocalRotate(new Vector3(0, 0, 0), 0.05F).OnComplete(() =>
            {
                OnClicked?.Invoke(this);
            });
        });
    }

    // Card flip animation to show back side...
    public void HideFrontSide()
    {
        transform.DOLocalRotate(new Vector3(0, 90, 0), 0.05F).OnComplete(() =>
        {
            SetCardBackSprite();
            transform.DOLocalRotate(new Vector3(0, 0, 0), 0.05F).OnComplete(() =>
            {
                _model.SetClosed();
            });
        });
    }
    
    private void SetCardFrontSprite()
    {
        cardSprite.sprite = _frontSprite;
    }
    
    private void SetCardBackSprite()
    {
        cardSprite.sprite = _backSprite;
    }
    
    public void OnCardButtonPressed()
    {
        if (_model.IsCardOpened()) return;
        _model.SetOpened();
        GameEvents.FireSoundRequested(SoundType.CardFlip);
        ShowFrontSide();
    }

    public int GetId()
    {
        return _model.Id;
    }

    public bool IsMatched()
    {
        return _model.IsCardMatched();
    }

    // if matched then animate to delete that cards...
    public void SetMatched()
    {
        transform.DOScale(Vector3.zero, 0.15F).SetDelay(0.25F).SetEase(Ease.InBack);
        _model.SetMatched();
    }
}