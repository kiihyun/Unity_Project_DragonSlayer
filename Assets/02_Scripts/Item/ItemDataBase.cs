using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : Singleton<ItemDatabase>
{
    [SerializeField] private List<ItemData> _itemDatabase;
    private Dictionary<int, ItemData> _itemDictionary = new Dictionary<int, ItemData>();

    protected override void Awake()
    {
        base.Awake();
        LoadItemDatabase();
        BuildDatabase();
    }

    private void BuildDatabase()
    {
        _itemDictionary.Clear();
        foreach (var item in _itemDatabase)
        {
            if (item != null && !_itemDictionary.ContainsKey(item.ItemID))
            {
                _itemDictionary.Add(item.ItemID, item);
            }
        }

    }

    public ItemData GetItemByID(int itemID)
    {
        _itemDictionary.TryGetValue(itemID, out ItemData item);
        if (item == null)
        {
            Debug.LogWarning($"아이템을 찾을 수 없습니다: {itemID}");
        }
        return item;
    }

    public ItemData CreateItemByID(int itemID)
    {
        if(itemID == -1)
        {
            return null;
        }
        
        return ScriptableObject.Instantiate(GetItemByID(itemID));
    }

    public void AddItemToDatabase(ItemData item)
    {
        if (item != null && !_itemDictionary.ContainsKey(item.ItemID))
        {
            _itemDatabase.Add(item);
            _itemDictionary.Add(item.ItemID, item);
        }
    }

    public void LoadItemDatabase()
    {
        _itemDatabase = new List<ItemData>(Resources.LoadAll<ItemData>("Items"));
    }
}