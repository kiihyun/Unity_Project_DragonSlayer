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


    public void Turn()
    {
        MoveDirection = -MoveDirection;
        FlipSprite();
    }
    private void FlipSprite()
    {
        bool _isMovingRight = MoveDirection.x > 0;
        _stateMachine.Enemy.SpriteRenderer.flipX = !_isMovingRight;
    }

    public void TurnLeft()
    {
        MoveDirection = Vector3.left;
        _stateMachine.Enemy.SpriteRenderer.flipX = false; // 왼쪽으로 돌 때 스프라이트를 왼쪽으로 뒤집음
    }
    public void TurnRight()
    {
        MoveDirection = Vector3.right;
        _stateMachine.Enemy.SpriteRenderer.flipX = true; // 오른쪽으로 돌 때 스프라이트를 오른쪽으로 뒤집음
    }
}
