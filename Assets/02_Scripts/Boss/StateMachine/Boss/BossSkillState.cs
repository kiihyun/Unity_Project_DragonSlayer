using UnityEngine;

public class BossSkillState : IBossState
{
    private BossEnemy _boss;
    private float _elapsedTime;
    private int _skillIndex;
    private bool _isSkillCasting;

    public BossSkillState(BossEnemy boss)
    {
        _boss = boss;
    }

    public void Enter()
    {
        Debug.Log("보스: 스킬 상태 진입");
        _elapsedTime = 0f;
        _isSkillCasting = false;
    }

    public void Execute()
    {
        var skills = _boss.IsPhase2 ? _boss.BossData.phase2Skills : _boss.BossData.phase1Skills;

        if (skills == null || skills.Count == 0)
        {
            Debug.LogWarning("보스 스킬 목록이 비어 있습니다.");
            _boss.StateMachine.ChangeState(new BossIdleState(_boss));
            return;
        }

        _elapsedTime += Time.deltaTime;
        var skill = skills[_skillIndex];

        if (!_isSkillCasting && _elapsedTime >= skill.delayBeforeCast)
        {
            CastSkill(skill);
            _isSkillCasting = true;
        }

        // 스킬 시전 후 다음 스킬로 넘어가기
        if (_isSkillCasting && _elapsedTime >= skill.delayBeforeCast + skill.cooldown)
        {
            _elapsedTime = 0f;
            _isSkillCasting = false;
            _skillIndex = (_skillIndex + 1) % skills.Count;

            _boss.StateMachine.ChangeState(new BossIdleState(_boss));
        }
    }

    public void Exit()
    {
        Debug.Log("보스: 스킬 상태 종료");
    }

    private void CastSkill(BossSkillData skill)
    {
        GameObject.Instantiate(skill.skillEffectPrefab, _boss.transform.position, Quaternion.identity);

        Debug.Log($"보스가 스킬 [{skill.skillName}] 시전! 데미지: {skill.damage}");
        // 여기에 애니메이션 트리거나 사운드도 연동 가능
    }
}