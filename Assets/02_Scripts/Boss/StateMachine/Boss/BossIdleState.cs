using UnityEngine;

public class BossIdleState : IBossState
{
    private BossEnemy _boss;
    private float _idleDelay = 0.5f; 
    private float _timer;
    private float _idleTimer;
    private float _skillCastInterval = 1f; // 1초마다 스킬 시도

    public BossIdleState(BossEnemy boss)
    {
        _boss = boss;
    }

    public void Enter()
    {
        Debug.Log("보스: 대기 상태 진입");
        
        _boss.Animator.SetBool("IsWalking", false);
        _boss.Animator.SetTrigger("Idle");
        _timer = _idleDelay;
    }

    public void Execute()
    {

        _boss.TryDetectPlayer();
        Debug.Log(_boss.PlayerTarget);
        _boss.FlipToFacePlayer();
        _idleTimer += Time.deltaTime;
        _timer -= Time.deltaTime;
        if (_timer <= 0f && _boss.IsPlayerInRange())
        {
            _boss.StateMachine.ChangeState(new BossAttackState(_boss));
            return; // 스킬 상태로 넘어가지 않게 막기
        }
        if (_idleTimer >= _skillCastInterval)
        {
            _boss.StateMachine.ChangeState(new BossSkillState(_boss));
        }
        
        // if (_boss.PlayerTarget != null)
        // {
        //     float dist = Vector2.Distance(_boss.transform.position, _boss.PlayerTarget.position);
        //     if (dist <= _boss.ChaseRange)
        //     {
        //         _boss.StateMachine.ChangeState(new BossChaseState(_boss));
        //     }
        // }
    }

    public void Exit()
    {
        Debug.Log("보스: 대기 상태 종료");
    }
}