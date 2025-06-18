using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interaction
{
    [SerializeField] private GameObject _closedSprite;
    [SerializeField] private GameObject _openedSprite;
    private bool _isOpen = false;
    
    private void Awake()
    {
        // 시작 시 닫힌 상태로 초기화
        SetSpriteState(_isOpen);
    }

    public override void Interact()
    {
        if (!_isOpen)
            Open();
    }

    public virtual void Open()
    {
        _isOpen = true;
        SetSpriteState(_isOpen);
        IsInteractable = false;
    }

    private void SetSpriteState(bool open)
    {
        _closedSprite.SetActive(!open);
        _openedSprite.SetActive(open);
    }
}