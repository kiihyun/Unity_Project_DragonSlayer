using System.Collections;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SkillType
{
    Default, 
    FireRain, // ���� �� ������ �ݺ� ����
    FlameMarch,
    Breath,
    SwordWind,
    LeapSmash,
    // ���� Meteor, Laser �� Ȯ�� ����
}

public class BossEnemy : MonoBehaviour, IDamageble
{
    [SerializeField] private BossEnemyDataSO _bossData;
    private float _currentHp;
    public Animator Animator { get; private set; }
    public BossStateMachine StateMachine { get; private set; }
    public BossEnemyDataSO BossData => _bossData;
    public Action OnBossDie;


    public int AttackCount { get; set; } = 0;
    public GameObject BreathPos;
    private Transform _playerTarget;
    public Transform PlayerTarget => _playerTarget;
    public BossSkillData CurrentSkillData { get; set; } //���� ���� ���� ��ų ������ ����

    public int SkillIndex { get; set; } = 0;

    public float MaxHealth { get; private set; }

    public float CurrentHealth { get; private set; }

    public int AttackThresholdBeforeSkill = 2; // �Ϲݰ��� Ƚ��
    private bool _facingRight = true; //����

    public Transform fireStartPoint; //FlameStomp
    public Rigidbody2D Rb { get; private set; }
    private SkinnedMeshRenderer[] meshRenderers;
    private SpriteRenderer spriteRenderer;

    [Header("Grounded Check")] [SerializeField]
    private Transform groundCheck;

    [SerializeField] private float groundRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        StateMachine = new BossStateMachine();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        CurrentHealth = _bossData.maxHP;
        StateMachine.Initialize(new BossIdleState(this));
    }

    private void Update()
    {
        TryDetectPlayer();
        StateMachine.Update();
    }


    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         _playerTarget = other.transform;
    //     }
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Test"))
    //     {
    //         _playerTarget = other.transform;
    //     }
    // }
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         _playerTarget = null;
    //     }
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Test"))
    //     {
    //         _playerTarget = null;
    //     }
    // }
    public bool IsPlayerInRange()
    {
        if (_playerTarget == null)
            return false;
        float sqrDistance = (transform.position - _playerTarget.position).sqrMagnitude;
        float sqrRange = _bossData.attackRange * _bossData.attackRange;

        return sqrDistance < sqrRange;
    }

    public void SpawnBreathEffect()
    {
        SoundManager.Instance.PlaySFX("droagon_fire_breathing");
        GameObject effect = Instantiate(
            BossData.phase1Skills[0].skillEffectPrefab, 
            BreathPos.transform.position, 
            Quaternion.Euler(0, 0, 90)
        );
        effect.transform.SetParent(this.transform);

        Destroy(effect, 0.7f); 
    }


    public void SpawnSkillEffect()
    {
        if (CurrentSkillData == null)
        {
            SpawnBasicHitEffect();
            return;
        }

        switch (CurrentSkillData.skillType)
        {
            case SkillType.FireRain:
                StartCoroutine(CastFireRain());
                break;
            case SkillType.FlameMarch:
                SpawnFlameMarchEffect();
                break;
            case SkillType.SwordWind:
                SpawnSwordWind();
                break;
            case SkillType.LeapSmash:
                StateMachine.ChangeState(new BossLeapState(this, CurrentSkillData));
                return;
            default:
                Debug.LogWarning($"���ǵ��� ���� SkillType: {CurrentSkillData.skillType}");
                break;
        }


        // Debug.Log($"[�ִϸ��̼� �̺�Ʈ] {CurrentSkillData.skillName} ����Ʈ ����!");
    }

    private void SpawnFireRainEffect()
    {
        if (CurrentSkillData == null) return;

        Vector3 spawnPos = CurrentSkillData.offsetFromBreathPos;
        Quaternion rotation = Quaternion.Euler(0, 0, CurrentSkillData.rotationZ);

        GameObject effect = Instantiate(CurrentSkillData.skillEffectPrefab, spawnPos, rotation);
        effect.transform.SetParent(this.transform);
        Destroy(effect, CurrentSkillData.effectDuration);
    }

    public void OnSkillAnimationComplete()
    {
        CurrentSkillData = null;
        var skills = BossData.phase1Skills;
        SkillIndex = (SkillIndex + 1) % BossData.phase1Skills.Count;
        StateMachine.ChangeState(new BossIdleState(this));
    }

    private IEnumerator CastFireRain()
    {
        SoundManager.Instance.PlaySFX("dragon_roar");
        
        for (int i = 0; i < 40; i++)
        {
            Vector3 spawnPos = new Vector3(
                transform.position.x + Random.Range(-20f, 20f),
                transform.position.y + 10f, 
                0f
            );

            GameObject fireRain = GameObject.Instantiate(BossData.phase1Skills[1].skillEffectPrefab, spawnPos,
                Quaternion.identity);
            fireRain.transform.SetParent(transform);

            Rigidbody2D rb = fireRain.GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(-50f, -3f).normalized; 
            float force = 5f;
            rb.AddForce(direction * force, ForceMode2D.Impulse);
            UnityEngine.Object.Destroy(fireRain, 10f); 

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void OnNormalAttackComplete()
    {
        AttackCount++;
        CurrentSkillData = null;
        SkillIndex = Random.Range(0, _bossData.phase1Skills.Count);
        StateMachine.ChangeState(new BossIdleState(this));

        if (AttackCount >= AttackThresholdBeforeSkill)
        {
            AttackCount = 0;
            StateMachine.ChangeState(new BossSkillState(this));
        }
        else
        {
            StateMachine.ChangeState(new BossIdleState(this));
        }
    }

    public void FlipToFacePlayer()
    {
        if (_playerTarget == null) return;

        float directionToPlayer = _playerTarget.position.x - transform.position.x;

        if ((directionToPlayer > 0 && _facingRight) || (directionToPlayer < 0 && !_facingRight))
        {
            _facingRight = !_facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void TryDetectPlayer()
    {
        if (PlayerTarget == null)
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, _bossData.detectionRange,
                LayerMask.GetMask("Player"));
            if (hit != null)
            {
                _playerTarget = hit.transform;
            }
        }
    }

    public void TriggerSkillEffect()
    {
        if (CurrentSkillData == null) return;

        switch (CurrentSkillData.skillName)
        {
            case "":
                SpawnFlameMarchEffect();
                break;
            case "BasicAttack":
                SpawnBasicHitEffect();
                break;
        }
    }

    private void SpawnBasicHitEffect()
    {
    }

    public void SpawnFlameMarchEffect()
    {
        SoundManager.Instance.PlaySFX("dragon_roar");
        
        StartCoroutine(FlameMarchRoutine(CurrentSkillData));
    }

    private IEnumerator FlameMarchRoutine(BossSkillData data)
    {
        Vector3 direction = _facingRight ? Vector3.right : Vector3.left;
        Vector3 startPos = fireStartPoint.position;

        for (int i = 0; i < data.flameCount; i++)
        {
            Vector3 spawnPos = startPos + direction * data.flameSpacing * i;

            GameObject flame = Instantiate(BossData.phase1Skills[2].skillEffectPrefab, spawnPos, Quaternion.identity);
            flame.transform.SetParent(this.transform);

            Destroy(flame, data.effectDuration);
            yield return new WaitForSeconds(data.flameInterval);
        }
    }


    public void TakeDamage(float damage)
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth -= damage;
            StartCoroutine(DamageFlash());
        }

        if (CurrentHealth <= 0)
        {
            StateMachine.ChangeState(new BossDieState(this));
            return;
        }
    }

    public void SpawnSwordWind()
    {
        GameObject effect = Instantiate(
            BossData.phase1Skills[0].skillEffectPrefab, 
            BreathPos.transform.position, 
            Quaternion.identity
        );
        effect.transform.SetParent(this.transform);

        Destroy(effect, BossData.phase1Skills[0].effectDuration); 
    }

    
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position,
            groundRadius,
            groundLayer);
    }

    public void SpawnLeap()
    {
        GameObject effect = Instantiate(
            BossData.phase1Skills[1].skillEffectPrefab, 
            groundCheck.transform.position + new Vector3(0, 0.6f, 0), 
            Quaternion.identity
        );
        effect.transform.SetParent(this.transform);

        Destroy(effect, BossData.phase1Skills[1].effectDuration); 
    }

    IEnumerator DamageFlash()
    {
            spriteRenderer.color = new Color(1.0f, 0.6f, 0.6f);

        yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
    }
}