using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // 적 사망 애니메이션 시작
        _stateMachine.Enemy.Animator.Play(_stateMachine.Enemy.AnimatorController.DieAnimationHash);
        _stateMachine.Enemy.Rigidbody.simulated = false;
    }
    public void OnAnimationEnd()
    {
        _stateMachine.Enemy.gameObject.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
