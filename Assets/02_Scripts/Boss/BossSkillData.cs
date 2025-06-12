using UnityEngine;

[System.Serializable]
public class BossSkillData
{
    public string skillName;
    public float damage;
    public float range;
    public float delayBeforeCast;
    public float cooldown;
    public GameObject skillEffectPrefab;
}