using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interaction
{
    [SerializeField] private GameObject _closedSprite;
    [SerializeField] private GameObject _openedSprite;
    private bool isOpen = false;
    
    private void Awake()
    {
        // 시작 시 닫힌 상태로 초기화
        SetSpriteState(isOpen);
    }

    public override void Interact()
    {
        if (!isOpen)
            Open();
    }

    public virtual void Open()
    {
        isOpen = true;
        SetSpriteState(isOpen);
        IsInteractable = false;
    }

    private void SetSpriteState(bool open)
    {
        _closedSprite.SetActive(!open);
        _openedSprite.SetActive(open);
    }
}