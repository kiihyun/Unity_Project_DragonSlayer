using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    [SerializeField]private EnemyAttack enemyAttack;
    

    private void Awake()
    {
        enemyAttack = GetComponentInChildren<EnemyAttack>();
    }
  

    public void Attack()
    {
        enemyAttack.OnAttackHit();
    }
}
