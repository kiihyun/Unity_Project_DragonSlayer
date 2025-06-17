using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemData> _consumableItems = new List<ItemData>();
    
    [SerializeField] private List<ItemData> _equipableItems = new List<ItemData>();
    
    [SerializeField] private ItemData[] _quickSlotItems = new ItemData[2];
    
    [SerializeField] private ItemData[] _equipSlotItems = new ItemData[2];
    
    
    // 외부에서 읽기만 가능하도록 제한
    public IReadOnlyList<ItemData> ConsumableItems => _consumableItems;
    public IReadOnlyList<ItemData> EquipableItems => _equipableItems;
    public IReadOnlyList<ItemData> QuickSlotItems => _quickSlotItems;
    public IReadOnlyList<ItemData> EquipSlotItems => _equipSlotItems;
    public event Action InventoryUpdate;
    
    
    // 인벤토리에 아이템 타입을 확인하고 추가
    public void AddItem(ItemData item)
    {
        if (item == null)
        {
            return;
        }

        if (item.ItemType == ItemType.Consumable)
        {
            _consumableItems.Add(item);
        }
        else if (item.ItemType == ItemType.Equipable)
        {
            _equipableItems.Add(item);
        }
        InventoryUpdate?.Invoke();
    }

    // 인벤토리의 아이템 타입을 확인하고 제거
    public void RemoveItem(ItemData item)
    {
        if (item == null)
        {
            return;
        }

        if (item.ItemType == ItemType.Consumable)
        {
            _consumableItems.Remove(item);
        }
        else if (item.ItemType == ItemType.Equipable)
        {
            _equipableItems.Remove(item);
        }
        InventoryUpdate?.Invoke();
    }

    // 퀵슬롯에 아이템 넣기, 빼기
    public void SetQuickSlotItem(int slot, ItemData item)
    {
        if (slot < 0 || slot >= _quickSlotItems.Length)
        {
            return;
        }
        
        // 기존 퀵슬롯 아이템을 인벤토리에 넣기
        ItemData prevItem = _quickSlotItems[slot];
        if (prevItem != null && !_consumableItems.Contains(prevItem))
        {
            AddItem(prevItem);
        }
        
        // 새로 넣는 아이템은 인벤토리에서 제거 (중복 방지)
        if (item != null && _consumableItems.Contains(item))
        {
            RemoveItem(item);
        }
        
        // 퀵슬롯에 아이템 넣기
        _quickSlotItems[slot] = item;
        
        InventoryUpdate?.Invoke();
    }

    // 장착슬롯에 아이템 넣기, 빼기
    public void SetEquipSlotItem(int slot, ItemData item)
    {
        if (slot < 0 || slot >= _equipSlotItems.Length)
        {
            return;
        }
        
        // 기존 장착슬롯 아이템을 인벤토리에 넣기 
        ItemData prevItem = _equipSlotItems[slot];
        if (prevItem != null && !_equipableItems.Contains(prevItem))
        {
            AddItem(prevItem);
        }
        
        // 새로 넣는 아이템은 인벤토리에서 제거 (중복 방지)
        if (item != null && _equipableItems.Contains(item))
        {
            RemoveItem(item);
        }
        
        // 장비슬롯에 아이템 넣기
        _equipSlotItems[slot] = item;
        
        InventoryUpdate?.Invoke();
    }
    
    
}
