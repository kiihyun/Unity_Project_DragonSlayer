using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipable
}

public enum StatType
{
    // Consumable
    Healing,
    
    // Equipable
    Health,
    AttackPower,
    Speed
}

[System.Serializable]
public class StatEntry
{
    public StatType type;
    public int value;
}

[System.Serializable]
public class ItemData : ScriptableObject
{
    public int ItemID;
    public string ItemName;
    public string ItemDescription;
    public Sprite ItemIcon;
    public ItemType ItemType;
    public List<StatEntry> stats;
}
