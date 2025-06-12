using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController
{
    [SerializeField]private string _idleAnimationName = "Idle";
    [SerializeField] private string _runAnimationName = "Run";
    [SerializeField] private string _attackAnimationName = "Attack";

    public int IdleAnimationHash { get; private set; }
    public int RunAnimationHash { get; private set; }
    public int AttackAnimationHash { get; private set; }

    public void Initialize()
    {
        IdleAnimationHash = Animator.StringToHash(_idleAnimationName);
        RunAnimationHash = Animator.StringToHash(_runAnimationName);
        AttackAnimationHash = Animator.StringToHash(_attackAnimationName);
    }

}
