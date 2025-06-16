using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IDamageble
{
    [field: SerializeField] public EnemyAnimatorController AnimatorController { get; private set; }
    [field: SerializeField] public string DebugCurrentState { get; private set; } // 현재 상태 이름을 저장하는 변수

    public Transform PlayerTransform;


    public Animator Animator;
    public Rigidbody2D Rigidbody { get; private set; }
    public EnemyStateMachine StateMachine => _stateMachine;

    public float MaxHealth => Data.Health;
    public float CurrentHealth { get { return _currentHealth; } }
    [SerializeField]private float _currentHealth;

    private EnemyStateMachine _stateMachine;
    public EnemySO Data;

    public Transform SpritePivot;

    public Collider2D DetectionCollider;
    public Collider2D LeftDetect;
    public Collider2D RightDetect;
    public Collider2D RangedAttackSensor;
    public Collider2D MainCollider;

    public bool RangedAttacked = false;


    public float _moveCooldown; // 이동 쿨타임 (초 단위)

    

    

    private void Awake()
    {
        _stateMachine = new EnemyStateMachine(this);
        SpritePivot = this.transform.Find("Sprite").GetComponent<Transform>();
        Animator = GetComponentInChildren<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        MainCollider = GetComponent<Collider2D>();
        AnimatorController.Initialize(); // 애니메이션 컨트롤러 초기화

        _stateMachine.ChangeState(_stateMachine.IdleState); // 초기 상태 설정
    }
    private void Start()
    {
        _currentHealth = MaxHealth; // 초기 체력 설정
        _moveCooldown = _stateMachine.Enemy.Data.MoveDelay;
    }

    // Update is called once per frame
    private void Update()
    {
        _stateMachine.Update();
        DebugCurrentState = _stateMachine.DebugCurrentState; // 상태 이름 업데이트

    }
    private void FixedUpdate()
    {
        
    }

    //피격시 color 하얗게?
    [ContextMenu("TakeDamage")]
    public void TestCode()
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
            Animator.SetTrigger("Hit");
            _stateMachine.GuardState.Turn();
            return;
        }
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

        _currentHealth -= damage;
        Animator.SetTrigger("Hit");
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    [ContextMenu("DieTest")]
    public void Die()
    {
        Debug.Log("Enemy died");
        MainCollider.enabled = false;
        _stateMachine.ChangeState(_stateMachine.DeathState);
    }

    public void TakeDamage(float damage)
    {
        TakeDamage1(damage);
    }
}
