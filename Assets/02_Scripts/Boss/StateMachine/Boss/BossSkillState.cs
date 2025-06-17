using System.Collections;
using UnityEngine;

public class BossSkillState : IBossState
{
    private BossEnemy _boss;
    private float _elapsedTime;
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
        var skills =  _boss.BossData.phase1Skills;

        if (skills == null || skills.Count == 0)
        {
            Debug.LogWarning("보스 스킬 목록이 비어 있습니다.");
            _boss.StateMachine.ChangeState(new BossIdleState(_boss));
            return;
        }
        
        _elapsedTime += Time.deltaTime;
        var skill = skills[_boss.SkillIndex];

        //중복시전 방지
        if (!_isSkillCasting && _elapsedTime >= skill.delayBeforeCast)
        {
            CastSkill(skill);
            _isSkillCasting = true;
        }

        // 스킬 시전 후 다음 스킬로 넘어가기
        // if (_isSkillCasting && _elapsedTime >= skill.delayBeforeCast + skill.cooldown)
        // {
        //     _elapsedTime = 0f;
        //     _isSkillCasting = false;
        //     _boss.SkillIndex = (_boss.SkillIndex + 1) % skills.Count;
        //
        //
        //     _boss.StateMachine.ChangeState(new BossIdleState(_boss));
        // }
    }

    public void Exit()
    {
        _boss.CurrentSkillData = null;
        Debug.Log("보스: 스킬 상태 종료");
    }

    private void CastSkill(BossSkillData skill)
    {
        _boss.CurrentSkillData = skill;
        if (skill.skillName == "FireRain")
        {
            _boss.Animator.SetTrigger(skill.animationTriggerName);
        }
        else if (skill.skillName == "breath")
        {
          _boss.Animator.SetTrigger(skill.animationTriggerName);
        }
        else if (skill.skillName == "FlameMarch")
        {
            _boss.Animator.SetTrigger(skill.animationTriggerName);
        }
        else if (skill.skillName == "SwordWind")
        {
            _boss.Animator.SetTrigger(skill.animationTriggerName);
        }

        Debug.Log($"보스가 스킬 [{skill.skillName}] 시전! 데미지: {skill.damage}");
    }

    
}