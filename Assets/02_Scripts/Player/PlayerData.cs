using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    // 스탯
    public int level;
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;
    public float dashPower;
    public float jumpPower;
    public int attackPower;
    public int currentExp;
    public int maxExp;

    // 인벤토리 (아이템ID 기반)
    public List<int> consumableItemIDs = new();
    public List<int> equipableItemIDs = new();
    public int[] quickSlotItemIDs = new int[2];
    public int[] equipSlotItemIDs = new int[2];
    
    // 스테이지 진행 정보
    public int maxClearStage;

    // 플레이어 위치
    public Vector3 position;
} 