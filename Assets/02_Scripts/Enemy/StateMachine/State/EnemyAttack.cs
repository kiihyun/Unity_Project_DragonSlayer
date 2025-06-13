using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Collider2D attackCollider;

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
        attackCollider.enabled = true;

        Invoke(nameof(DisableAttackCollider), 0.1f); // 1프레임만 유효하게 하려면 짧게
    }

    private void DisableAttackCollider()
    {
        attackCollider.enabled = false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //데미지 로직
        }
    }
}
