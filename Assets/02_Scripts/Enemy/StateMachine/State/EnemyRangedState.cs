using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedState : EnemyBaseState
{
    private Vector2 directionToPlayer;
    

    public EnemyRangedState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Enemy.AnimatorController.RangedAnimationHash);
        _stateMachine.Enemy.RangedAttackSensor.enabled = true;
    }

    

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Enemy.AnimatorController.RangedAnimationHash);
        _stateMachine.Enemy.RangedAttackSensor.enabled = false;
    }


    public override void Update()
    {
        base.Update();
        directionToPlayer = _stateMachine.Enemy.PlayerTransform.position - _stateMachine.Enemy.transform.position; // 플레이어와 적의 위치 차이 계산
        if (directionToPlayer.x < 0)
        {
            TurnLeft(); // 왼쪽으로 돌기
        }
        // 2. 플레이어가 오른쪽에 있고, 오른쪽이 발판 위라면
        else if (directionToPlayer.x > 0)
        {
            TurnRight(); // 오른쪽으로 돌기
        }
    }
}
