using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine _stateMachine;
    protected Vector2 _moveVector;
    protected Vector3 _moveDirection = Vector3.left;
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
        Vector2 moveVec = _moveDirection.normalized * _stateMachine.Enemy.Data._speed;

        _stateMachine.Enemy.Rigidbody.velocity = new Vector2(moveVec.x, _stateMachine.Enemy.Rigidbody.velocity.y);
    }


    public void Turn()
    {
        _moveDirection = -_moveDirection;
        FlipSprite();
    }
    private void FlipSprite()
    {
        bool _isMovingRight = _moveDirection.x > 0;
        _stateMachine.Enemy.SpriteRenderer.flipX = !_isMovingRight;
    }
}
