using System.Collections;
using UnityEngine;
public enum SkillType
{
    Default,    // 정적 프리팹 생성 (breath 등)
    FireRain,   // 여러 개 프리팹 반복 생성
    FlameMarch,
    Breath,
    // 향후 Meteor, Laser 등 확장 가능
}
public class BossEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private BossEnemyDataSO _bossData;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _chaseRange = 20f;
    [SerializeField] private float _stopDistance = 10f;
    public float ChaseRange => _chaseRange;
    public float StopDistance => _stopDistance;
    public float MoveSpeed => _moveSpeed;
    private float _currentHp;
    public Animator Animator { get; private set; }
    public BossStateMachine StateMachine { get; private set; }
    public BossEnemyDataSO BossData => _bossData;

    public bool IsPhase2 => _currentHp <= _bossData.maxHP * (_bossData.phase2ThresholdPercent / 100f);
    
    public int AttackCount { get; set; } = 0;
    public GameObject BreathPos;
    private Transform _playerTarget;
    public Transform PlayerTarget => _playerTarget;
    public BossSkillData CurrentSkillData { get; set; } //현재 시전 중인 스킬 정보를 저장

    public int SkillIndex { get; set; } = 0;
    public int AttackThresholdBeforeSkill = 3;// 일반공격 횟수
    private bool _facingRight = true; //방향
    
    public Transform fireStartPoint; //FlameStomp


    private void Awake()
    {
        Animator = GetComponent<Animator>();
        StateMachine = new BossStateMachine();
    }

    private void Start()
    {
        _currentHp = _bossData.maxHP;
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
        GameObject effect = Instantiate(
            BossData.attackEffectPrefab, // SO에 연결된 이펙트 프리팹
                BreathPos.transform.position , // 보스 앞쪽
            Quaternion.Euler(0, 0, 90)
        );
        effect.transform.SetParent(this.transform);

        Destroy(effect, 0.7f); // 일정 시간 후 파괴
    }


    public void TakeDamage(int dmg)
    {
        _currentHp -= dmg;
        
        if (_currentHp <= 0)
        {
            StateMachine.ChangeState(new BossDieState(this));
            return;
        }
    }
    public void SpawnSkillEffect() 
    {
        if (CurrentSkillData == null) return;

        switch (CurrentSkillData.skillType)
        {
            case SkillType.Breath:
                SpawnBreathEffect();
                break;
            case SkillType.FireRain:
                StartCoroutine(CastFireRain(20,0.1f,CurrentSkillData));
                break;
            case SkillType.FlameMarch:
                SpawnFlameMarchEffect();
                break;
            case SkillType.Default:
                SpawnBasicHitEffect();
                break;
            default:
                Debug.LogWarning($"정의되지 않은 SkillType: {CurrentSkillData.skillType}");
                break;
        }
        

        Debug.Log($"[애니메이션 이벤트] {CurrentSkillData.skillName} 이펙트 생성!");
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
        var skills = IsPhase2 ? BossData.phase2Skills : BossData.phase1Skills;
        SkillIndex = (SkillIndex + 1) % BossData.phase1Skills.Count;
        StateMachine.ChangeState(new BossIdleState(this));
    }
    
    private IEnumerator CastFireRain(int count, float interval, BossSkillData skill)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = new Vector3(
                transform.position.x + Random.Range(-15f, 15f),
                transform.position.y + 10f, // 하늘 위
                0f
            );

            GameObject fireRain = GameObject.Instantiate(skill.skillEffectPrefab, spawnPos, Quaternion.identity);
            fireRain.transform.SetParent(transform);

            // Rigidbody2D에 사선 힘 주기
            Rigidbody2D rb = fireRain.GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(-50f, -3f).normalized; // 사선 방향
            float force = 5f;
            rb.AddForce(direction * force, ForceMode2D.Impulse);
            UnityEngine.Object.Destroy(fireRain, 10f); // 파괴 시간 설정

            yield return new WaitForSeconds(interval);
        }
    }
    //일반 공격후 스킬시전
    public void OnNormalAttackComplete()
    {
        AttackCount++;

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
    //플레이어 바라보기
    public void FlipToFacePlayer()
    {
        if (_playerTarget == null) return;

        float directionToPlayer = _playerTarget.position.x - transform.position.x;

        if ((directionToPlayer > 0 && _facingRight) || (directionToPlayer < 0 && !_facingRight))
        {
            _facingRight = !_facingRight;
            // 보스 자체 반전
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
    public void TryDetectPlayer()
    {
        if (PlayerTarget == null)
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, _bossData.detectionRange , LayerMask.GetMask("Test"));
            if (hit != null)
            {
                _playerTarget = hit.transform;
                Debug.Log("플레이어 감지됨!");
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
        StartCoroutine(FlameMarchRoutine(CurrentSkillData));
    }

    private IEnumerator FlameMarchRoutine(BossSkillData data)
    {
        Vector3 direction = _facingRight ? Vector3.right : Vector3.left;
        Vector3 startPos = fireStartPoint.position;

        for (int i = 0; i < data.flameCount; i++)
        {
            Vector3 spawnPos = startPos + direction * data.flameSpacing * i;

            GameObject flame = Instantiate(data.skillEffectPrefab, spawnPos, Quaternion.identity);
            flame.transform.SetParent(this.transform);

            Destroy(flame, data.effectDuration);
            yield return new WaitForSeconds(data.flameInterval);
        }
    }
}