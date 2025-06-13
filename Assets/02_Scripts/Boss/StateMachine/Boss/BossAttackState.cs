using UnityEngine;

public class BossAttackState : IBossState
{
    private BossEnemy _boss;
    private float _attackCooldown;
    private bool _hasAttacked;


    public BossAttackState(BossEnemy boss)
    {
        _boss = boss;
        _attackCooldown = 5.1f; // 공격 후 딜레이 (초)
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
        if (BossTestPlayer.Instance == null)
            return;

        Vector3 diff = _boss.transform.position - BossTestPlayer.Instance.transform.position;
        float sqrDistance = diff.sqrMagnitude;
        float sqrAttackRange = _boss.BossData.attackRange * _boss.BossData.attackRange;

        if (sqrDistance > sqrAttackRange)
        {
            Debug.Log("플레이어가 공격 범위 밖에 있음");
            _boss.StateMachine.ChangeState(new BossIdleState(_boss));
            return;
        }

        string attackTrigger;

        if (_boss.AttackCount >= 2)
        {
            // 강한 공격: Attack2
            attackTrigger = "Attack2";
            _boss.AttackCount = 0; // 카운트 초기화
        }
        else
        {
            // 일반 공격: Attack1
            attackTrigger = "Attack1";
            _boss.AttackCount++;
        }

        //_boss.Animator.ResetTrigger("Idle");
        _boss.Animator.SetTrigger(attackTrigger);

        BossTestPlayer.Instance.TakeDamage(_boss.BossData.attackDamage);
        Debug.Log($"보스가 {attackTrigger} 시전! 데미지: {_boss.BossData.attackDamage}");

        _hasAttacked = true;
    }

}