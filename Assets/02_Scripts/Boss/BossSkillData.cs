using UnityEngine;

[System.Serializable]
public class BossSkillData
{
    public string skillName;
    public SkillType skillType;
    public float damage;
    public float range;
    public float delayBeforeCast;
    public float cooldown;
    public GameObject skillEffectPrefab;
    public Vector3 offsetFromBreathPos; // 스킬별 이펙트 위치 차이
    public float rotationZ; // 이펙트 회전 각도
    public string animationTriggerName; // 연결된 애니메이션 트리거 이름
    public float effectDuration; // 이펙트 유지 시간
    
}