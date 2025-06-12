using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public EnemySO Data;
    [SerializeField] private EnemyStateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new EnemyStateMachine(this);
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        _stateMachine.Update();
    }
    private void FixedUpdate()
    {
        
    }
}
