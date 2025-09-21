using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    [SerializeField]private Enemy _enemy;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_enemy.RangedAttacked == true)
                return;

            _enemy.StateMachine.ChangeState(_enemy.StateMachine.AttackState);
            _enemy.RangedAttacked = true;
        }
    }
}
