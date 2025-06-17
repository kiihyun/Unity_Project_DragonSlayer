using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireRain : MonoBehaviour
{
    public float fallSpeed = 5f;
    public float _damage = 10f;

    public void Init(float damage)
    {
        _damage = damage;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.GetComponent<Player>()?.stat.TakeDamage(_damage);
        }
    }
}