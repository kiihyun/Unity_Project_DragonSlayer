using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    [field:SerializeField]public EnemyAnimatorController AnimatorController { get; private set; }


    [SerializeField] private string _debugCurrentState;


    public Transform PlayerTransform;


    public Animator Animator;
    public Rigidbody2D Rigidbody { get; private set; }
    public EnemyStateMachine StateMachine => _stateMachine;

    private EnemyStateMachine _stateMachine;
    public EnemySO Data;
    public SpriteRenderer SpriteRenderer;

    public Collider2D DetectionCollider;


    public Collider2D LeftDetect;
    public Collider2D RightDetect;

    private Vector3 _moveDirection = Vector3.left;


    public float _moveCooldown; // 이동 쿨타임 (초 단위)

    public int maxHealth => Data._health;
    public int currentHealth { get; private set; }

    

    private void Awake()
    {
        _stateMachine = new EnemyStateMachine(this);
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Animator = GetComponentInChildren<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        AnimatorController.Initialize(); // 애니메이션 컨트롤러 초기화

        _stateMachine.ChangeState(_stateMachine.IdleState); // 초기 상태 설정
    }
    private void Start()
    {
        currentHealth = maxHealth; // 초기 체력 설정
        _moveCooldown = _stateMachine.Enemy.Data._moveDelay;
    }

    // Update is called once per frame
    private void Update()
    {
        _stateMachine.Update();
        _debugCurrentState = _stateMachine.DebugCurrentState; // 상태 이름 업데이트

    }
    private void FixedUpdate()
    {
        
    }

    //피격시 color 하얗게?
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        // 적 사망 로직
        Debug.Log("Enemy died");
        // 예: 애니메이션 재생, 오브젝트 비활성화 등
        _stateMachine.ChangeState(_stateMachine.DeathState);
    }


}
