using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    [SerializeField]private float _moveCooldown; // 이동 쿨타임 (초 단위)
    [SerializeField] private bool _checkGround;
    private float _curTime = 0f;
    

    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }




    public override void Enter()
    {
        base.Enter();
        //StartAnimation(stateMachine.IdleAnimationHash);
        _moveCooldown = _stateMachine.Enemy.Data._moveDelay;
    }
    public override void Update()
    {
        base.Update();
        //if (enterchasingstate)
            _stateMachine.ChangeState(_stateMachine.ChasingState);
        _curTime += Time.deltaTime;
        if (_curTime >= _moveCooldown)// 1초에 1번씩 움직임
        {
            _curTime = 0f; // 쿨타임 초기화
                           // 이동 로직 추가
            Move();
        }



    }
    public override void Exit()
    {
        base.Exit();
        //StopAnimation(stateMachine.IdleAnimationHash);
    }
    
}
