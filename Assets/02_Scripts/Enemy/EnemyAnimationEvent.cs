using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    [SerializeField] private EnemyAttack _enemyAttack;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private EnemyProjectile _enemyProjectile;

    private void Awake()
    {
        _enemyAttack = GetComponentInChildren<EnemyAttack>();
        _enemy = GetComponentInParent<Enemy>();
        _gameObject = _enemy.gameObject;
    }


    public void ObjectOff()
    {
        //_gameObject.SetActive(false);
        Destroy(_gameObject);
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

    public void ShootProjectile()
    {
        _projectile.transform.localPosition = new Vector2(0, -0.3f);
        _enemyProjectile.CurTime = 0f;
        _projectile.SetActive(true);
        
        if (Mathf.Approximately(this.transform.localRotation.y, 0f))
        {
            Debug.Log("Leftshoot");
            _enemyProjectile.IsLeft = true;
        }
        else
        {
            Debug.Log("Rightshoot");
            _enemyProjectile.IsLeft = false;
        }
    }
}
