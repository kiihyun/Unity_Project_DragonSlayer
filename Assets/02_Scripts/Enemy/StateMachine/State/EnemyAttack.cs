using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private Enemy _enemy;
    public GameObject TestRedBox;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        attackCollider.enabled = false;
        TestRedBox.SetActive(false); // 시작 시 빨간 박스 비활성화
    }

    // 애니메이션 이벤트에서 호출
    public void OnAttackHit()
    {
        attackCollider.enabled = true;
        TestRedBox.SetActive(true); // 공격 시 빨간 박스 활성화
        Invoke(nameof(DisableAttackCollider), 0.1f); // 1프레임만 유효하게 하려면 짧게
    }

    private void DisableAttackCollider()
    {
        attackCollider.enabled = false;
        TestRedBox.SetActive(false); // 공격 후 빨간 박스 비활성화
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamageable>(out var player))
        {
            player.TakeDamage(_enemy.Data.AttackDamage);
        }
    }
}
