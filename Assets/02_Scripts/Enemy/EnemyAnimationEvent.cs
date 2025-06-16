using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    [SerializeField] private EnemyAttack _enemyAttack;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameObject GameObject;

    private void Awake()
    {
        _enemyAttack = GetComponentInChildren<EnemyAttack>();
    }
  

    public void ObjectOff()
    {
        GameObject.SetActive(false);
    }
    
    public void ToAttackState()
    {
        _enemy.StateMachine.ChangeState(_enemy.StateMachine.AttackState);
    }

    public void ToChasingState()
    {
        _enemy.StateMachine.ChangeState(_enemy.StateMachine.ChasingState);
    }

    public void Attack()
    {
        _enemyAttack.OnAttackHit();
    }
    
    public void ToRangedState()
    {
        _enemy.StateMachine.ChangeState(_enemy.StateMachine.RangedState);
    }
}
