using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private Enemy _enemy;

    private bool _hasDamaged = false;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        attackCollider.enabled = false;
    }

    // 애니메이션 이벤트에서 호출
    public void OnAttackHit()
    {
        _hasDamaged = false;
        attackCollider.enabled = true;
        Invoke(nameof(DisableAttackCollider), 0.1f); // 1프레임만 유효하게 하려면 짧게
    }

    private void DisableAttackCollider()
    {
        attackCollider.enabled = false;
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_hasDamaged)
            return; // 이미 데미지 준 상태면 무시
        if (collision.TryGetComponent<IDamageble>(out var target))
        {
            target.TakeDamage(_enemy.Data.AttackDamage);
            _hasDamaged = true; // 이번 공격에선 딱 한 번만 데미지 줌
        }
    }
}
