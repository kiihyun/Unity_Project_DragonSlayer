using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IDamageable    
{
    [field:SerializeField]public EnemyAnimatorController AnimatorController { get; private set; }
    [field:SerializeField]public string DebugCurrentState { get; private set; } // 현재 상태 이름을 저장하는 변수

    public Transform PlayerTransform;


    public Animator Animator;
    public Rigidbody2D Rigidbody { get; private set; }
    public EnemyStateMachine StateMachine => _stateMachine;

    private EnemyStateMachine _stateMachine;
    public EnemySO Data;

    public Transform SpritePivot;

    public Collider2D DetectionCollider;


    public Collider2D LeftDetect;
    public Collider2D RightDetect;



    public float _moveCooldown; // 이동 쿨타임 (초 단위)

    public int maxHealth => Data.Health;
    public int currentHealth { get; private set; }

    

    private void Awake()
    {
        _stateMachine = new EnemyStateMachine(this);
        SpritePivot = this.transform.Find("Sprite").GetComponent<Transform>();
        Animator = GetComponentInChildren<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        AnimatorController.Initialize(); // 애니메이션 컨트롤러 초기화

        _stateMachine.ChangeState(_stateMachine.IdleState); // 초기 상태 설정
    }
    private void Start()
    {
        currentHealth = maxHealth; // 초기 체력 설정
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




    private void TakeDamage(int damage)
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

        currentHealth -= damage;
        Animator.SetTrigger("Hit");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    [ContextMenu("DieTest")]
    public void Die()
    {
        Debug.Log("Enemy died");
        _stateMachine.ChangeState(_stateMachine.DeathState);
    }

    void IDamageable.TakeDamage(int damage)
    {
        this.TakeDamage(damage);
    }
}
