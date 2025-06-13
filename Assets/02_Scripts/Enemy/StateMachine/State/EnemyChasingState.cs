using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }


    public override void Enter()
    {
        base.Enter();
        _stateMachine.Enemy.Animator.Play(_stateMachine.Enemy.AnimatorController.ChaseAnimationHash);
    }

    public override void Exit() 
    {
        base.Exit(); 
    }

    public override void Update()
    {
        base.Update();
        // Chase 상태에서의 로직을 여기에 작성
        // 예: 플레이어를 추적하는 로직
        /*
        if (_stateMachine.Enemy.Target != null)
        {
            _moveDirection = ( - _stateMachine.Enemy.transform.position).normalized;
            Move();
        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
         */
    }
}
