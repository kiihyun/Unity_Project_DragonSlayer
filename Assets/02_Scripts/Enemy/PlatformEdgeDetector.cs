using UnityEngine;

public class PlatformEdgeDetector : MonoBehaviour
{
    [SerializeField] private EnemyStateMachine _stateMachine;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Collider2D _collider;
    private void Awake()
    {
        
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _stateMachine = GetComponentInParent<Enemy>().StateMachine;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        
        if (!_collider.IsTouchingLayers())
        {
            _rigidbody.velocity = Vector3.zero; // 속도 초기화
            _stateMachine.IdleState.Turn();
        }
    }
}
