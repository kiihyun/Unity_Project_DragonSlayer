using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TreasureChest : Chest
{
    [SerializeField] private List<ItemData> _itemPrefabs;
    
    public override void Interact()
    {
        base.Interact();
    }   

    public override void Open()
    {
        base.Open();

        // 아이템 생성
        foreach(var item in _itemPrefabs)
        {
            Inventory inventory = _player.GetComponent<Inventory>();
            inventory.AddItem(item);
        }

        StartCoroutine(ShowItemGetUI());

        InteractText = "";
    }

    public IEnumerator ShowItemGetUI()
    {
        var interactionUI = _player.GetComponentInChildren<PlayerInteractionUI>();
        foreach(var item in _itemPrefabs)
        {
            interactionUI.ShowItemGetUI(item);
            yield return new WaitForSeconds(Constants.Interaction.ITEM_GET_UI_INTERVAL);
        }
    }
}