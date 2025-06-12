using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private EnemyStateMachine _stateMachine;
    [SerializeField] private EnemyAnimatorController _animatorController;
    public EnemySO Data;
    public SpriteRenderer SpriteRenderer;
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Collider2D LeftDetect;
    public Collider2D RightDetect;

    public int maxHealth => Data._health;
    public int currentHealth { get; private set; }

    private void Awake()
    {
        _stateMachine = new EnemyStateMachine(this);
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Animator = GetComponentInChildren<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        currentHealth = maxHealth; // 초기 체력 설정
    }

    // Update is called once per frame
    private void Update()
    {
        _stateMachine.Update();
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
        gameObject.SetActive(false);
    }
}
