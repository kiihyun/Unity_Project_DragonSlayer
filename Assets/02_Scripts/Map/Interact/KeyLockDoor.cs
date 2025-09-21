using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class KeyLockDoor : Door
{
    [SerializeField] private GameObject _lockSprite;
    [SerializeField] private GameObject _unlockSprite;
    private bool _isLock = true;
    [SerializeField] private ItemData _keyItem;

    public override string GetInteractText()
    {
        if(_isLock)
        {
            return "열쇠가 필요해";
        }
        else
        {
            return "E를 눌러 상호작용";
        }
    }

    public override void Interact()
    {
        if(_isLock)
        {
            if(_player.GetComponent<Player>().inventory.HasItem(_keyItem.ItemID))
            {
                _isLock = false;
                if(_lockSprite != null)
                {
                    _lockSprite.SetActive(false);
                }
                if(_unlockSprite != null)
                {
                    _unlockSprite.SetActive(true);
                }
            }

            return;
        }

        if (_player != null && IsInteractable)
        {
            StartCoroutine(MoveToOppositeDoor());

        }
    }

}