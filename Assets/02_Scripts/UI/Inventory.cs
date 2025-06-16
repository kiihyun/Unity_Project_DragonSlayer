using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<ItemData> consumableItems = new List<ItemData>();
    [SerializeField] private List<ItemData> equipableItems = new List<ItemData>();

    // 외부에서 읽기만 가능하도록 제한
    public IReadOnlyList<ItemData> ConsumableItems => consumableItems;
    public IReadOnlyList<ItemData> EquipableItems => equipableItems;
    public event Action InventoryUpdate;
    
    
    // 인벤토리에 아이템 타입을 확인하고 추가
    public void AddItem(ItemData item)
    {
        if (item == null) return;
        if (item.ItemType == ItemType.Consumable)
            consumableItems.Add(item);
        else if (item.ItemType == ItemType.Equipable)
            equipableItems.Add(item);
        InventoryUpdate?.Invoke();
    }

    // 인벤토리의 아이템 타입을 확인하고 제거
    public void RemoveItem(ItemData item)
    {
        if (item == null) return;
        if (item.ItemType == ItemType.Consumable)
            consumableItems.Remove(item);
        else if (item.ItemType == ItemType.Equipable)
            equipableItems.Remove(item);
        InventoryUpdate?.Invoke();
    }
    
}
