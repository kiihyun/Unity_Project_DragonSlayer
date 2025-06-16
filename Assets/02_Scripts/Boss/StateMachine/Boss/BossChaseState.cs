using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : IBossState
{
    private BossEnemy _boss;

    public BossChaseState(BossEnemy boss)
    {
        _boss = boss;
    }

    public void Enter()
    {
        Debug.Log("보스: 추적 상태 진입");
        _boss.Animator.SetBool("IsWalking", true); // 걷기 시작
    }

    public void Execute()
    {
        if (_boss.PlayerTarget == null) return;

        _boss.FlipToFacePlayer();
        float distance = Vector2.Distance(_boss.transform.position, _boss.PlayerTarget.position);

        // 너무 멀면 추적 중단
        if (distance > _boss.ChaseRange)
        {
            _boss.Animator.SetBool("IsWalking", false);
            _boss.StateMachine.ChangeState(new BossIdleState(_boss));
            return;
        }

        // 충분히 가까우면 추적 중단 (공격 등 다음 행동 가능)
        if (distance <= _boss.StopDistance)
        {
            _boss.Animator.SetBool("IsWalking", false);
            _boss.StateMachine.ChangeState(new BossIdleState(_boss));
            return;
        }

        // 그 외 거리에서는 계속 추적
        Vector3 targetPos = new Vector3(_boss.PlayerTarget.position.x, _boss.transform.position.y, 0);
        _boss.transform.position = Vector3.MoveTowards(
            _boss.transform.position,
            targetPos,
            _boss.MoveSpeed * Time.deltaTime
        );
    }

    public void Exit()
    {
        Debug.Log("보스: 추적 상태 종료");
        _boss.Animator.SetBool("IsWalking", false); // 걷기 중지
    }
}