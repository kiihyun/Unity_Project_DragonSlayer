using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    public Vector3 MoveDirection = Vector3.left;
    protected EnemyStateMachine _stateMachine;
    protected Vector2 _moveVector;
    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void Update()
    {

    }
    public virtual void Move()
    {
        Vector2 moveVec = MoveDirection.normalized * _stateMachine.Enemy.Data.Speed;

        _stateMachine.Enemy.Rigidbody.velocity = new Vector2(moveVec.x, _stateMachine.Enemy.Rigidbody.velocity.y);

        bool _isMovingRight = moveVec.x > 0;
        if (_isMovingRight)
        {
            _stateMachine.Enemy.SpritePivot.transform.localRotation = Quaternion.Euler(0, 180, 0); // 오른쪽으로 이동할 때 스프라이트를 기본 방향으로 설정
        }
        else
        {
            _stateMachine.Enemy.SpritePivot.transform.localRotation = Quaternion.Euler(0, 0, 0); // 왼쪽으로 이동할 때 스프라이트를 뒤집음
        }

    }
    

    public void Turn()
    {
        _stateMachine.Enemy.MoveCount = 0;
        MoveDirection = -MoveDirection;
        FlipSprite();
    }
    

    public void TurnLeft()
    {
        MoveDirection = Vector3.left;
        _stateMachine.Enemy.SpritePivot.transform.localRotation = Quaternion.Euler(0, 0, 0); // 왼쪽으로 이동할 때 스프라이트를 뒤집음
    }
    public void TurnRight()
    {
        MoveDirection = Vector3.right;
        _stateMachine.Enemy.SpritePivot.transform.localRotation = Quaternion.Euler(0, 180, 0); // 오른쪽으로 이동할 때 스프라이트를 기본 방향으로 설정
    }
    private void FlipSprite()
    {
        bool _isMovingRight = MoveDirection.x > 0;
        if (_isMovingRight)
        {
            _stateMachine.Enemy.SpritePivot.transform.localRotation = Quaternion.Euler(0, 180, 0); // 오른쪽으로 이동할 때 스프라이트를 기본 방향으로 설정
        }
        else
        {
            _stateMachine.Enemy.SpritePivot.transform.localRotation = Quaternion.Euler(0, 0, 0); // 왼쪽으로 이동할 때 스프라이트를 뒤집음
        }
    }

    protected void StartAnimation(int animatorHash)
    {
        _stateMachine.Enemy.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        _stateMachine.Enemy.Animator.SetBool(animatorHash, false);
    }
}
