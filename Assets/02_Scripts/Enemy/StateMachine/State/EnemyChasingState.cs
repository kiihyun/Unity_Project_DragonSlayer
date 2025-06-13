using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{ 
    private float _curTime = 0f;
    private Vector2 directionToPlayer; // 플레이어와 적의 위치 차이
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }


    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Enemy.AnimatorController.ChaseAnimationHash); // 추적 상태 애니메이션 시작
    }

    public override void Exit() 
    {
        base.Exit(); 
        StopAnimation(_stateMachine.Enemy.AnimatorController.ChaseAnimationHash); // 추적 상태 애니메이션 중지
    }

    public override void Update()
    {
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

        if (directionToPlayer.magnitude <= _stateMachine.Enemy.Data._attackRange)   //사거리보다 가까우면?
        {
            _stateMachine.ChangeState(_stateMachine.AttackState); //공격
        }




        
        

        _curTime += Time.deltaTime;
        if (_curTime >= _stateMachine.Enemy._moveCooldown*0.7)// 0.7초에 1번씩 움직임
        {
            _curTime = 0f; // 쿨타임 초기화
            Move();
        }
    }
}
