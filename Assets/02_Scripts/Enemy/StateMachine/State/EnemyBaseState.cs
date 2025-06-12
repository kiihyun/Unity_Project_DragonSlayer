using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine _stateMachine;
    protected Vector2 _moveVector;
    protected Vector3 _moveDirection = Vector3.left;
    protected bool _isMovingRight = false;
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


    protected void Turn()
    {
        _moveDirection = -_moveDirection;
        FlipSprite();
    }
    protected void FlipSprite()
    {
        _isMovingRight = !_isMovingRight;
        _stateMachine.Enemy.SpriteRenderer.flipX = _isMovingRight;
    }

    protected void StartAnimation(int animationHash)
    {
        _stateMachine.Enemy.Animator.SetBool(animationHash, true);
    }
    protected void StopAnimation(int animationHash)
    {
        _stateMachine.Enemy.Animator.SetBool(animationHash, false);
    }
}
