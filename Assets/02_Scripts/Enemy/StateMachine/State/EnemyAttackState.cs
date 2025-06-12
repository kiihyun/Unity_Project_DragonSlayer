using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //StartAnimation(stateMachine.AttackAnimationHash);
    }
    public override void Exit() 
    {
        base.Exit();
        //StopAnimation(stateMachine.AttackAnimationHash);
    }
    public override void Update()
    {
        base.Update();
        // Attack 상태에서의 로직을 여기에 작성
        // 예: 공격 애니메이션이 끝나면 Idle 상태로 전환
        //if (stateMachine.IsAttackAnimationFinished())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
