using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemData> InventoryItems { get { return inventoryItems; }
    }
    private List<ItemData> inventoryItems =  new List<ItemData>();
    
    
    // 인벤토리에 아이템 추가
    public void AddItem(ItemData item)
    {
        inventoryItems.Add(item);
    }
    
    //인벤토리에서 아이템 제거
    public void RemoveItem(ItemData item)
    {
        inventoryItems.Remove(item);
    }
    
}
