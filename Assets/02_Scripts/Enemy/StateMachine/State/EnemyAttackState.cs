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
        StartAnimation(_stateMachine.Enemy.AnimatorController.AttackAnimationHash); // 공격 애니메이션 시작
    }
    public override void Exit() 
    {
        base.Exit();
        StopAnimation(_stateMachine.Enemy.AnimatorController.AttackAnimationHash); // 공격 애니메이션 중지
    }
    public override void Update()
    {
    }
}
