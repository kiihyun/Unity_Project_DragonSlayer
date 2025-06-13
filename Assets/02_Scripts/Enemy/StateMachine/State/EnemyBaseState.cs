using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine _stateMachine;
    protected Vector2 _moveVector;
    public Vector3 MoveDirection = Vector3.left;
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
        Vector2 moveVec = MoveDirection.normalized * _stateMachine.Enemy.Data._speed;

        _stateMachine.Enemy.Rigidbody.velocity = new Vector2(moveVec.x, _stateMachine.Enemy.Rigidbody.velocity.y);
    }
    protected void StartAnimation(int animatorHash)
    {
        _stateMachine.Enemy.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        _stateMachine.Enemy.Animator.SetBool(animatorHash, false);
    }

    public void Turn()
    {
        MoveDirection = -MoveDirection;
        FlipSprite();
    }
    private void FlipSprite()
    {
        bool _isMovingRight = MoveDirection.x > 0;
        if(_isMovingRight)
        {
            _stateMachine.Enemy.SpritePivot.transform.localRotation = Quaternion.Euler(0, 180, 0); // 오른쪽으로 이동할 때 스프라이트를 기본 방향으로 설정
        }
        else
        {
            _stateMachine.Enemy.SpritePivot.transform.localRotation = Quaternion.Euler(0, 0, 0); // 왼쪽으로 이동할 때 스프라이트를 뒤집음
        }
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
}
