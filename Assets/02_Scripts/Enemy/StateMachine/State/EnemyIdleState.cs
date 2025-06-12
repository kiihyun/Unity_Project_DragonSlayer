using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }




    public override void Enter()
    {
        base.Enter();
        //StartAnimation(stateMachine.IdleAnimationHash);
    }
    public override void Update()
    {
        base.Update();
        // Idle 상태에서의 로직을 여기에 작성
        stateMachine.ChangeState(stateMachine.ChasingState);
        
    }
    public override void Exit()
    {
        base.Exit();
        //StopAnimation(stateMachine.IdleAnimationHash);
    }
}
