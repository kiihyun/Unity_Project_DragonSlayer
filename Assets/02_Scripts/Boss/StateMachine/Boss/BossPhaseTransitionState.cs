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

        // _boss.SetInvincible(true);
        _elapsedTime = 0f;

        /*// ���� ����: ��� ���
        if (_boss.BossData.phase2Dialogues != null && _boss.BossData.phase2Dialogues.Count > 0)
        {
            string line = _boss.BossData.phase2Dialogues[0]; // �Ǵ� ����
            Debug.Log($"���� ���: \"{line}\"");
        }*/

        // TODO: ī�޶� ����ũ, ����Ʈ, �ִϸ��̼� �� �߰� ����
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
    }
}