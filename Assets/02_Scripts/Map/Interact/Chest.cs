using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interaction
{
    [SerializeField] private GameObject closedSprite;
    [SerializeField] private GameObject openedSprite;
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
        closedSprite.SetActive(!open);
        openedSprite.SetActive(open);
    }
}