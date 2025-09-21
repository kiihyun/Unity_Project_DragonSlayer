using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    
    [SerializeField] private bool _checkGround;
    private float _curTime = 0f;
    

    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }




    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Enemy.AnimatorController.IdleAnimationHash);
    }
    public override void Update()
    {
        base.Update();
        //if (enterchasingstate)
        //    _stateMachine.ChangeState(_stateMachine.ChasingState);
        _curTime += Time.deltaTime;
        if (_curTime >= _stateMachine.Enemy._moveCooldown)// 1초에 1번씩 움직임
        {
            _curTime = 0f; // 쿨타임 초기화
                           // 이동 로직 추가
            if (_stateMachine.Enemy.MoveCount > 2)
            {
                Turn();
            }
            Move();
            _stateMachine.Enemy.MoveCount++;
        }
        


    }
    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Enemy.AnimatorController.IdleAnimationHash);
    }
    
}
