using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IDamageble
{
    [field: SerializeField] public EnemyAnimatorController AnimatorController { get; private set; }
    [field: SerializeField] public string DebugCurrentState { get; private set; } // 현재 상태 이름을 저장하는 변수
    [SerializeField] private float _currentHealth;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public Transform PlayerTransform;

    public Animator Animator;
    public Rigidbody2D Rigidbody { get; private set; }
    public EnemyStateMachine StateMachine => _stateMachine;
    public EnemySO Data;

    public Transform SpritePivot;

    public Collider2D DetectionCollider;
    public Collider2D LeftDetect;
    public Collider2D RightDetect;
    public Collider2D RangedAttackSensor;
    public Collider2D MainCollider;

    public int MoveCount = 0;
    public bool RangedAttacked = false;
    public bool IsDead;
    public float MoveCooldown; // 이동 쿨타임 (초 단위)
    public float MaxHealth => Data.Health;
    public float CurrentHealth { get { return _currentHealth; } }

    private EnemyStateMachine _stateMachine;
    private CapsuleCollider2D _capsuleCollider;


    

    

    private void Awake()
    {
        _stateMachine = new EnemyStateMachine(this);
        SpritePivot = this.transform.Find("Sprite").GetComponent<Transform>();
        Animator = GetComponentInChildren<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        MainCollider = GetComponent<Collider2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        AnimatorController.Initialize(); // 애니메이션 컨트롤러 초기화

        _stateMachine.ChangeState(_stateMachine.IdleState); // 초기 상태 설정\
        
    }
    private void Start()
    {
        _currentHealth = MaxHealth; // 초기 체력 설정
        MoveCooldown = _stateMachine.Enemy.Data.MoveDelay;
        IsDead = false;
    }

    // Update is called once per frame
    private void Update()
    {
        _stateMachine.Update();
        DebugCurrentState = _stateMachine.DebugCurrentState; // 상태 이름 업데이트

    }
        
    private void TakeDamage1(float damage)
    {
        if (DebugCurrentState == "EnemyGuardState")
        {
            Vector2 MonsterToPlayer = PlayerTransform.position - this.transform.position;

            float yRotation = SpritePivot.localEulerAngles.y;
            if (MonsterToPlayer.x > 0 && Mathf.Approximately(yRotation, 180f))
            {
                Animator.SetTrigger("Block");
                return;
            }
            else if (MonsterToPlayer.x < 0 && Mathf.Approximately(yRotation, 0f))
            {
                Animator.SetTrigger("Block");
                return;
            }
            _stateMachine.GuardState.Turn();
        }
        if (IsDead)
        {
            return;
        }
        _currentHealth -= damage;
        StartCoroutine(HitFlash());
        if (_currentHealth <= 0)
        {
            Die();
            IsDead = true;
        }
    }

    [ContextMenu("DieTest")]
    public void Die()
    {
        UIManager.instance.player.stat.GainExp(Data.Experience);
        int playerLayerMask = 1 << LayerMask.NameToLayer("Player");
        _capsuleCollider.excludeLayers |= playerLayerMask;
        _stateMachine.ChangeState(_stateMachine.DeathState);

    }

    public void TakeDamage(float damage)
    {
        TakeDamage1(damage);
    }



    private IEnumerator HitFlash()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.color = Color.white;
    }
}
