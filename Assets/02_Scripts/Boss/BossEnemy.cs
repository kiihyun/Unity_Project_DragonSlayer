using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [SerializeField] private BossEnemyDataSO _bossData;
    private float _currentHp;

    private void Start()
    {
        _currentHp = _bossData.maxHP;
        Debug.Log($"[보스 등장] {_bossData.bossName} 체력: {_bossData.maxHP}");
        // 필요한 컴포넌트들 초기화 (UI, AI 등)
    }

    public void TakeDamage(float dmg)
    {
        _currentHp -= dmg;
        if (_currentHp <= _bossData.maxHP * (_bossData.phase2ThresholdPercent / 100f))
        {
            Debug.Log("▶ 페이즈 2 돌입!");
            // AIController.FSM 상태 전환 등 호출
        }

        if (_currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{_bossData.bossName} 처치됨!");
        // 클리어 연출, 보상 지급
    }
}