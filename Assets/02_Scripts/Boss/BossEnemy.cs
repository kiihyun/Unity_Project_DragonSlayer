using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [SerializeField] private BossEnemyDataSO _bossData;
    private float _currentHp;
    public Animator Animator { get; private set; }
    public BossStateMachine StateMachine { get; private set; }
    public BossEnemyDataSO BossData => _bossData;

    public bool IsPhase2 => _currentHp <= _bossData.maxHP * (_bossData.phase2ThresholdPercent / 100f);
    
    public int AttackCount { get; set; } = 0;


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
        StateMachine.Update();
    }

    public void TakeDamage(float dmg)
    {
        _currentHp -= dmg;
        Animator.SetTrigger("Hurt");

        if (_currentHp <= 0)
        {
            StateMachine.ChangeState(new BossDieState(this));
        }
    }

    public bool IsPlayerInRange()
    {
        float sqrDistance = (transform.position - BossTestPlayer.Instance.transform.position).sqrMagnitude;
        float sqrRange = _bossData.detectionRange * _bossData.detectionRange;

        return sqrDistance < sqrRange;
    }

}