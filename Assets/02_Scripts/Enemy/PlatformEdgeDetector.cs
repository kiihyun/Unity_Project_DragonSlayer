 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEdgeDetector : MonoBehaviour
{
    [SerializeField] private EnemyStateMachine _stateMachine;
    private void Awake()
    {
        _stateMachine = GetComponentInParent<Enemy>().StateMachine;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_stateMachine.CurrentState is EnemyIdleState idleState)
        {
            idleState.Turn();
        }
    }
}
