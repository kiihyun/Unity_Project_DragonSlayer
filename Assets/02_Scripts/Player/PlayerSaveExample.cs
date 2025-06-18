using UnityEngine;
using System.Linq;

public class PlayerSaveExample : MonoBehaviour
{
    public Player player;

    [ContextMenu("Save Player Data")]
    public void SavePlayer()
    {
        if (player == null)
        {
            Debug.LogError("[PlayerSaveExample] Player가 할당되지 않았습니다.");
            return;
        }

        PlayerStat stat = player.GetComponent<PlayerStat>();
        Inventory inventory = player.GetComponent<Inventory>();

        if (stat == null || inventory == null)
        {
            Debug.LogError("[PlayerSaveExample] PlayerStat 또는 Inventory 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        PlayerData data = new PlayerData();
        data.level = stat.Level;
        data.maxHealth = stat.MaxHealth;
        data.currentHealth = stat.CurrentHealth;
        data.moveSpeed = stat.MoveSpeed;
        data.dashPower = stat.DashPower;
        data.jumpPower = stat.JumpPower;
        data.attackPower = (int)stat.AttackPower;
        data.currentExp = stat.CurrentEXP;
        data.maxExp = stat.MaxExp;

        data.consumableItemIDs = inventory.ConsumableItems.Select(item => item.ItemID).ToList();
        data.equipableItemIDs = inventory.EquipableItems.Select(item => item.ItemID).ToList();
        data.quickSlotItemIDs = inventory.QuickSlotItems.Select(item => item != null ? item.ItemID : -1).ToArray();
        data.equipSlotItemIDs = inventory.EquipSlotItems.Select(item => item != null ? item.ItemID : -1).ToArray();

        // 스테이지 진행 정보 저장
        data.maxClearStage = StageManager.Instance.GetMaxClearStage();

        // 플레이어 위치 저장
        data.position = player.transform.position;

        SaveManager.Instance.Save("player", data);
        Debug.Log("[PlayerSaveExample] PlayerData 저장 완료");
    }
} 