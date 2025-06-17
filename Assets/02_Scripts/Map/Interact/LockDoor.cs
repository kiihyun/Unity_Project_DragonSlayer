using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LockDoor : Door
{
    [SerializeField] private GameObject _lockSprite;
    [SerializeField] private GameObject _unlockSprite;
    private bool _isLock = true;
    [SerializeField] private ItemData _keyItem;

    public override void Interact()
    {
        if(_isLock)
        {
            if(_player.GetComponent<Player>().inventory.HasItem(_keyItem.ItemID))
            {
                _isLock = false;
                _lockSprite.SetActive(false);
                _unlockSprite.SetActive(true);
            }

            return;
        }

        if (_player != null && IsInteractable)
        {
            StartCoroutine(MoveToOppositeDoor());

        }
    }

}