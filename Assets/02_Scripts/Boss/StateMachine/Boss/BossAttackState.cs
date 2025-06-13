using UnityEngine;

public class BossAttackState : IBossState
{
    private BossEnemy _boss;
    private float _attackCooldown;
    private bool _hasAttacked;

    public BossAttackState(BossEnemy boss)
    {
        _boss = boss;
        _attackCooldown = 1.5f; // 공격 후 딜레이 (초)
        _hasAttacked = false;
    }

    public void Enter()
    {
        Debug.Log("보스: 공격 상태 진입");
        Attack();
    }

    public void Execute()
    {
        if (_hasAttacked)
        {
            _attackCooldown -= Time.deltaTime;
            if (_attackCooldown <= 0f)
            {
                _boss.StateMachine.ChangeState(new BossIdleState(_boss));
            }
        }
    }

    public void Exit()
    {
        Debug.Log("보스: 공격 상태 종료");
    }

    private void Attack()
    {
        // 1. 플레이어 존재 여부 확인
        if (BossTestPlayer.Instance == null)
        {
            Debug.LogWarning("플레이어가 존재하지 않습니다.");
            return;
        }

        // 2. 거리 계산
        float distance = Vector3.Distance(_boss.transform.position, BossTestPlayer.Instance.transform.position);

        // 3. 사정거리 안이면 공격
        if (distance <= _boss.BossData.attackRange)
        {
            // 4. 데미지 주기
            BossTestPlayer.Instance.TakeDamage(_boss.BossData.attackDamage);

            // 5. 로그 출력
            Debug.Log($"보스가 플레이어를 공격! 데미지: {_boss.BossData.attackDamage}");

            // 6. 공격 완료 처리
            _hasAttacked = true;

            // 애니메이션 트리거
            _boss.Animator.SetTrigger("Attack");
        }
        else
        {
            // 사정거리 밖이면 공격 취소 → Idle로 복귀
            Debug.Log("플레이어가 공격 범위 밖에 있음");
            _boss.StateMachine.ChangeState(new BossIdleState(_boss));
        }
    }
}