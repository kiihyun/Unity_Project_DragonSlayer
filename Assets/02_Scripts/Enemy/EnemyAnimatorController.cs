using UnityEngine;


[System.Serializable]
public class EnemyAnimatorController
{
    [SerializeField] private string _idleAnimationName = "Idle";
    [SerializeField] private string _attackAnimationName = "Attack";
    [SerializeField] private string _dieAnimationName = "Die";
    [SerializeField] private string _chaseAnimationName = "Chase";
    [SerializeField] private string _guardAnimationName = "Guard";

    public int IdleAnimationHash { get; private set; }
    public int RunAnimationHash { get; private set; }
    public int AttackAnimationHash { get; private set; }
    public int DieAnimationHash { get; private set; }
    public int ChaseAnimationHash { get; private set; }
    public int GuardAnimationHash { get; private set; }

    public void Initialize()
    {
        IdleAnimationHash = Animator.StringToHash(_idleAnimationName);
        AttackAnimationHash = Animator.StringToHash(_attackAnimationName);
        DieAnimationHash = Animator.StringToHash(_dieAnimationName);
        ChaseAnimationHash = Animator.StringToHash(_chaseAnimationName);
        GuardAnimationHash = Animator.StringToHash(_guardAnimationName);
    }
}
