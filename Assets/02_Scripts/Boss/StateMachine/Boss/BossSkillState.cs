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
        _elapsedTime = 0f;
        _isSkillCasting = false;
    }

    public void Execute()
    {
        var skills =  _boss.BossData.phase1Skills;

        if (skills == null || skills.Count == 0)
        {
            Debug.LogWarning("���� ��ų ����� ��� �ֽ��ϴ�.");
            _boss.StateMachine.ChangeState(new BossIdleState(_boss));
            return;
        }
        
        _elapsedTime += Time.deltaTime;
        var skill = skills[_boss.SkillIndex];

        if (!_isSkillCasting && _elapsedTime >= skill.delayBeforeCast)
        {
            CastSkill(skill);
            _isSkillCasting = true;
        }

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
    }

    private void CastSkill(BossSkillData skill)
    {
        Debug.Log($"[CastSkill] index:{skill} " +
                  $"name:{skill.skillName} type:{skill.skillType}");
        _boss.CurrentSkillData = skill;   
        switch (skill.skillType)
        {
            case SkillType.LeapSmash:                              
                _boss.StateMachine.ChangeState(
                    new BossLeapState(_boss, skill));              
                return;                                            

            case SkillType.Breath:
                _boss.Animator.SetTrigger(skill.animationTriggerName);
                break;

            case SkillType.FireRain:
                _boss.Animator.SetTrigger(skill.animationTriggerName);
                break;

            case SkillType.FlameMarch:
                _boss.Animator.SetTrigger(skill.animationTriggerName);
                break;

            case SkillType.SwordWind:
                _boss.Animator.SetTrigger(skill.animationTriggerName);
                break;

            default:
                Debug.LogWarning($"���ǵ��� ���� SkillType : {skill.skillType}");
                break;
        }

    }

    
}