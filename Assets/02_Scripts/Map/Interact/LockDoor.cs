using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LockDoor : Door
{
    [SerializeField] private GameObject _lockSprite;
    [SerializeField] private GameObject _unlockSprite;
    private bool _isLock = true;

    public override void Update()
    {
        // 임시
        if(Input.GetKeyDown(KeyCode.E) && player != null)
        {
            Interact();
        }
    }

    public override void Interact()
    {
        if(_isLock)
        {
            if(true)
            // if(player.inventory.HasItem(ItemData.id))
            {
                _isLock = false;
                _lockSprite.SetActive(false);
                _unlockSprite.SetActive(true);
            }

            return;
        }

        if (player != null && IsInteractable)
        {
            StartCoroutine(MoveToOppositeDoor());

        }
    }

}