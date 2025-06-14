using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemData> InventoryItems { get { return inventoryItems; }
    }
    private List<ItemData> inventoryItems =  new List<ItemData>();
    
    
}
