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
        _stateMachine.Enemy.Animator.Play(_stateMachine.Enemy.AnimatorController.AttackAnimationHash);
    }
    public override void Exit() 
    {
        base.Exit();
        //StopAnimation(stateMachine.AttackAnimationHash);
    }
    public override void Update()
    {
    }
}
