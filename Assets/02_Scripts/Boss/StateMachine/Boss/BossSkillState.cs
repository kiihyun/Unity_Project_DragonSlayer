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
        Debug.Log($"[CastSkill] index:{skill} " +
                  $"name:{skill.skillName} type:{skill.skillType}");
        _boss.CurrentSkillData = skill;   // 이후 SpawnSkillEffect 에서 참조

        switch (skill.skillType)
        {
            /* ――― 도약(Leap Smash) → 별도 State ――― */
            case SkillType.LeapSmash:                              // ← enum에 추가
                _boss.StateMachine.ChangeState(
                    new BossLeapState(_boss, skill));              // ★
                return;                                            // 여기서 바로 종료

            /* ――― Breath ――― */
            case SkillType.Breath:
                _boss.Animator.SetTrigger(skill.animationTriggerName);
                break;

            /* ――― Fire Rain ――― */
            case SkillType.FireRain:
                _boss.Animator.SetTrigger(skill.animationTriggerName);
                break;

            /* ――― Flame March ――― */
            case SkillType.FlameMarch:
                _boss.Animator.SetTrigger(skill.animationTriggerName);
                break;

            /* ――― Sword Wind ――― */
            case SkillType.SwordWind:
                _boss.Animator.SetTrigger(skill.animationTriggerName);
                break;

            /* ――― 예외 처리 ――― */
            default:
                Debug.LogWarning($"정의되지 않은 SkillType : {skill.skillType}");
                break;
        }

        Debug.Log($"보스가 스킬 [{skill.skillName}] 시전! 데미지: {skill.damage}");
    }

    
}