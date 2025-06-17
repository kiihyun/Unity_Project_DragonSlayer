using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingChanger : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var play = collision.GetComponent<Transform>();
            _enemy.PlayerTransform = play; // 플레이어를 적에게 할당

            if (_enemy.Data.RangeAttackable)
            {
                _enemy.StateMachine.ChangeState(_enemy.StateMachine.RangedState);
                return;
            }

            _enemy.StateMachine.ChangeState(_enemy.StateMachine.ChasingState);
            return;
        }
        /*
        if (collision.TryGetComponent<Transform>(out var player))
        {
            _enemy.PlayerTransform = player; // 플레이어를 적에게 할당
            _enemy.StateMachine.ChangeState(_enemy.StateMachine.ChasingState);
        }
        */
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _enemy.PlayerTransform = collision.transform;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            _enemy.StateMachine.ChangeState(_enemy.StateMachine.IdleState);
        }
    }
}
