using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuardState : EnemyBaseState
{
    public EnemyGuardState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("GuardState");
        StartAnimation(_stateMachine.Enemy.AnimatorController.GuardAnimationHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Enemy.AnimatorController.GuardAnimationHash);
    }


    public override void Update()
    {
        base.Update();
    }


}
