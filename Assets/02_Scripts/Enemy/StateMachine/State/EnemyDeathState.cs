using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _stateMachine.Enemy.Animator.SetTrigger(_stateMachine.Enemy.AnimatorController.DieAnimationHash); // 죽음 애니메이션 시작
    }
    

    public override void Exit()
    {
        base.Exit();
    }

}
