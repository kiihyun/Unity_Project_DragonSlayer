using UnityEngine;

public class PlatformEdgeDetector : MonoBehaviour
{
    [SerializeField] private EnemyStateMachine _stateMachine;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private bool _isLeft; // 회전 속도 조절
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
            if(_rigidbody.velocity.x >0)
            {
                _isLeft = true;
            }
            else if (_rigidbody.velocity.x < 0)
            {
                _isLeft = false;
            }
            _rigidbody.velocity = Vector3.zero; // 속도 초기화
            _stateMachine.IdleState.Turn();
            if(_isLeft)
            {
                _rigidbody.velocity = Vector2.left * 1;
            }
            else
            {
                _rigidbody.velocity = Vector2.right * 1;
            }
        }
    }
}
