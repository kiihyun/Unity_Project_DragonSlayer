using UnityEngine;

public class BossPhaseTransitionState : IBossState
{
    private BossEnemy _boss;
    private float _transitionTime;
    private float _elapsedTime;

    public BossPhaseTransitionState(BossEnemy boss)
    {
        _boss = boss;
        _transitionTime = boss.BossData.phaseTransitionInvincibleTime;
    }

    public void Enter()
    {
        Debug.Log("보스: 페이즈 전환 상태 진입");

        // _boss.SetInvincible(true);
        _elapsedTime = 0f;

        /*// 연출 예시: 대사 출력
        if (_boss.BossData.phase2Dialogues != null && _boss.BossData.phase2Dialogues.Count > 0)
        {
            string line = _boss.BossData.phase2Dialogues[0]; // 또는 랜덤
            Debug.Log($"보스 대사: \"{line}\"");
        }*/

        // TODO: 카메라 쉐이크, 이펙트, 애니메이션 등 추가 가능
    }

    public void Execute()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _transitionTime)
        {
            // _boss.IsPhase2 = true;
            // _boss.SetInvincible(false);

            _boss.StateMachine.ChangeState(new BossIdleState(_boss));
        }
    }

    public void Exit()
    {
        Debug.Log("보스: 페이즈2 전환 완료");
    }
}