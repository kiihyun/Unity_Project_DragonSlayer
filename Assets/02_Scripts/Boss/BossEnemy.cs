using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [SerializeField] private BossEnemyDataSO _bossData;
    private float _currentHp;
    
    public BossStateMachine StateMachine { get; private set; }
    public BossEnemyDataSO BossData => _bossData;
    
    public bool IsPhase2 => _currentHp <= _bossData.maxHP * (_bossData.phase2ThresholdPercent / 100f);


    private void Awake()
    {
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

        if (_currentHp <= 0)
        {
            StateMachine.ChangeState(new BossDieState(this));
        }
    }

    /*public bool IsPlayerInRange()
    {
        // 감지 범위 체크 로직
        //return Vector3.Distance(transform.position, Player.Instance.transform.position) < _bossData.detectionRange;
    }*/
}
