using System.Collections;
using System.Collections.Generic;
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
        // GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        // item.transform.SetParent(transform);
    }
}