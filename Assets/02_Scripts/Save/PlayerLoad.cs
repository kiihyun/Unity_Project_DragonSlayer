using UnityEngine;

public class PlayerLoad : MonoBehaviour
{
    [SerializeField] private Player _player;
    private StageManager _stageManager;

    private void Awake()
    {
        _stageManager = StageManager.Instance;
        LoadManger.Instance._playerLoad = this;
    }

    [ContextMenu("Load Player Data")]
    public void LoadPlayer()
    {
        PlayerData data = LoadManger.Instance.Load<PlayerData>("player");
        if (data == null)
        {
            Debug.LogError("[PlayerLoadExample] PlayerData를 불러오지 못했습니다.");
            return;
        }
        
        PlayerStat playerStat = _player.stat;
        playerStat.Init();

        // 플레이어 스탯 데이터 로드
        playerStat.SetPlayerStat(
            data.maxHealth,
            data.currentHealth, 
            data.moveSpeed, 
            data.dashPower, 
            data.jumpPower, 
            data.attackPower, 
            data.level, 
            data.currentExp);

        // 인벤토리 데이터 로드
        _player.inventory.AllClear();

        foreach (int itemID in data.consumableItemIDs)
        {
            _player.inventory.AddItem(ItemDatabase.Instance.CreateItemByID(itemID));
        }
        foreach (int itemID in data.equipableItemIDs)
        {
            _player.inventory.AddItem(ItemDatabase.Instance.CreateItemByID(itemID));
        }

        _player.inventory.SetQuickSlotItem(0, ItemDatabase.Instance.CreateItemByID(data.quickSlotItemIDs[0]));
        _player.inventory.SetQuickSlotItem(1, ItemDatabase.Instance.CreateItemByID(data.quickSlotItemIDs[1]));

        _player.inventory.SetEquipSlotItem(0, ItemDatabase.Instance.CreateItemByID(data.equipSlotItemIDs[0]));
        _player.inventory.SetEquipSlotItem(1, ItemDatabase.Instance.CreateItemByID(data.equipSlotItemIDs[1]));

        Debug.Log($"[PlayerLoadExample] PlayerData 불러오기 완료\n" +
            $"레벨: {data.level}\n" +
            $"HP: {data.currentHealth} / {data.maxHealth}\n" +
            $"이동속도: {data.moveSpeed}\n" +
            $"대시: {data.dashPower}, 점프: {data.jumpPower}\n" +
            $"공격력: {data.attackPower}\n" +
            $"경험치: {data.currentExp} / {data.maxExp}\n" +
            $"소비아이템: [{string.Join(", ", data.consumableItemIDs)}]\n" +
            $"장비아이템: [{string.Join(", ", data.equipableItemIDs)}]\n" +
            $"퀵슬롯: [{string.Join(", ", data.quickSlotItemIDs)}]\n" +
            $"장착슬롯: [{string.Join(", ", data.equipSlotItemIDs)}]\n" +
            $"최대 클리어 스테이지: {data.maxClearStage}\n" +
            $"플레이어 위치: {data.position}\n"
        );

        // 위치 적용
        if (_player != null)
        {
            _player.transform.position = data.position;
            Debug.Log($"[PlayerLoadExample] 플레이어 위치 적용 완료: {data.position}");
        }

        if(_stageManager == null) _stageManager = StageManager.Instance;

        StageManager.Instance.SetMaxClearStage(data.maxClearStage);
    }
} 