using UnityEngine;

public class BossDieState : IBossState
{
    private BossEnemy _boss;
    private bool _hasDied;

    public BossDieState(BossEnemy boss)
    {
        _boss = boss;
    }

    public void Enter()
    {
        if (_hasDied) return;

        Debug.Log($"보스 {_boss.BossData.bossName} 사망 상태 진입");

        // 애니메이션 트리거
        Animator animator = _boss.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // 이펙트 출력 (예: 폭발)
        if (_boss.BossData.bossPrefab != null)
        {
            // 죽을 때 쓸 이펙트 프리팹 따로 만들 수도 있음
            Debug.Log("보스 사망 이펙트 발생");
        }

        // 사망 후 움직임 멈춤 (필요 시 Rigidbody2D, NavMesh 등 처리)
        Rigidbody2D rb = _boss.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

        // 보상 지급 (예: 경험치, 아이템 등)
        GiveRewards();

        // 클리어 처리 (UI 활성화 등)
        _boss.OnBossDie?.Invoke();
        _hasDied = true;
    }

    public void Execute()
    {
        // 아무 것도 하지 않음
    }

    public void Exit()
    {
        // 이 상태에선 보통 Exit 안 씀
    }

    private void GiveRewards()
    {
        Debug.Log("보상 지급: 경험치 + 아이템 드랍 등");
        
        // 예: GameManager.Instance.AddExp(300);
        // 예: Instantiate(dropItem, _boss.transform.position, Quaternion.identity);
    }
}