using UnityEngine;

public class BossIdleState : IBossState
{
    private BossEnemy _boss;
    private float _idleDelay = 0.5f; 
    private float _timer;

    public BossIdleState(BossEnemy boss)
    {
        _boss = boss;
    }

    public void Enter()
    {
        Debug.Log("보스: 대기 상태 진입");
        _boss.Animator.SetTrigger("Idle");
        _timer = _idleDelay;
    }

    public void Execute()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f && _boss.IsPlayerInRange())
        {
            _boss.StateMachine.ChangeState(new BossAttackState(_boss));
        }
    }

    public void Exit()
    {
        Debug.Log("보스: 대기 상태 종료");
    }
}