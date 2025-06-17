using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBossEnemyData", menuName = "Enemy/Boss/BossEnemyData")]
public class BossEnemyDataSO : ScriptableObject
{
    [Header("기본 정보")]
    public string bossName;
    public int bossNum;
    public float maxHP;
    public float moveSpeed;

    [Header("보스 외형")]
    public Sprite bossPortrait;
    public GameObject bossPrefab;

    [Header("보스 전투 설정")]
    public float attackDamage;
    public float attackRange;
    public float detectionRange;
    public float phase2ThresholdPercent;
    public GameObject attackEffectPrefab;
    public float ChaseRange;
    public float StopDistance;
    public float NormalAttackDamageDelay   = 0.8f;
    public float normalAttackCooldown = 4.0f;
    
    [Header("페이즈1 스킬")]
    public List<BossSkillData> phase1Skills;

    [Header("페이즈2 스킬")]
    public List<BossSkillData> phase2Skills;
    
    [Header("대사/연출")]
    public List<string> phase1Dialogues;
    public List<string> phase2Dialogues;
    public AudioClip bossBGM;

    [Header("페이즈 전환 관련")]
    public float phaseTransitionInvincibleTime = 2.0f;
}