using UnityEngine;

public class PlayerLoadExample : MonoBehaviour
{
    [SerializeField] private Player _player;
    private PlayerStat playerStat;
    private Inventory inventory;
    private PlayerData data;

    private void Awake()
    {
        playerStat = _player.GetComponent<PlayerStat>();
        inventory = _player.GetComponent<Inventory>();
    }

    [ContextMenu("Load Player Data")]
    public void LoadPlayer()
    {
        PlayerData data = SaveManager.Instance.Load<PlayerData>("player");
        if (data == null)
        {
            Debug.LogError("[PlayerLoadExample] PlayerData를 불러오지 못했습니다.");
            return;
        }
        

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
    }
} 