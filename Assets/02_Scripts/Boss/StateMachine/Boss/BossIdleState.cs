using UnityEngine;

public class BossIdleState : IBossState
{
    private BossEnemy _boss;
    private float _idleDelay = 0.5f; 
    private float _idleTimer;
    private float _skillCastInterval = 4f; // 1초마다 스킬 시도
    float _attackReadyTimer = 0f;

    public BossIdleState(BossEnemy boss)
    {
        _boss = boss;
    }

    public void Enter()
    {
        Debug.Log("보스: 대기 상태 진입");
        
        _boss.Animator.SetBool("IsWalking", false);
        _boss.Animator.SetTrigger("Idle");
    }

    public void Execute()
    {

        _boss.TryDetectPlayer();
        Debug.Log(_boss.PlayerTarget);
        _boss.FlipToFacePlayer();
        _idleTimer += Time.deltaTime;
        _attackReadyTimer += Time.deltaTime;

        if (_boss.IsPlayerInRange() &&
            _attackReadyTimer >= _boss.BossData.normalAttackCooldown)
        {
            _attackReadyTimer = 0f;
            _boss.StateMachine.ChangeState(new BossAttackState(_boss));
        }
        if (_idleTimer >= _skillCastInterval)
        {
            _boss.StateMachine.ChangeState(new BossSkillState(_boss));
        }
        
        if (_boss.PlayerTarget != null)
        {
            float dist = Vector2.Distance(_boss.transform.position, _boss.PlayerTarget.position);

            // 멀리 있으면 추적 시작
            if (dist > _boss.BossData.StopDistance && dist <= _boss.BossData.ChaseRange)
            {
                _boss.StateMachine.ChangeState(new BossChaseState(_boss));
                return;
            }
        }
    }

    public void Exit()
    {
        Debug.Log("보스: 대기 상태 종료");
    }
}