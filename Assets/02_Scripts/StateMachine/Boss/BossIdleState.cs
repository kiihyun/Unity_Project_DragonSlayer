using UnityEngine;

public class BossIdleState : IBossState
{
    private BossEnemy _boss;

    public BossIdleState(BossEnemy boss)
    {
        _boss = boss;
    }

    public void Enter()
    {
        Debug.Log("보스: 대기 상태 진입");
    }

    public void Execute()
    {
        // 일정 시간 뒤 공격 상태로 전환 등
        /*if (_boss.IsPlayerInRange())
        {
            _boss.StateMachine.ChangeState(new BossAttackState(_boss));
        }*/
    }

    public void Exit()
    {
        Debug.Log("보스: 대기 상태 종료");
    }
}