using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float _velocity;
    [SerializeField] private Enemy _enemy;
    public bool IsLeft;
    public float CurTime;
    private float _projectileTimer = 2f;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }
    // Update is called once per frame
    void Update()
    {
        CurTime += Time.deltaTime;
        if (CurTime > _projectileTimer)
        {
            this.gameObject.SetActive(false);
            return;
        }
        if (IsLeft)
            this.transform.position += Vector3.left * _velocity; 
        else
            this.transform.position += Vector3.right * _velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamageble>(out var player))
        {
            player.TakeDamage(_enemy.Data.AttackDamage);
            this.gameObject.SetActive(false);
        }

    }
}
