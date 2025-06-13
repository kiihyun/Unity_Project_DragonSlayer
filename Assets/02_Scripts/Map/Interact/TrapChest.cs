using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapChest : Chest
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private int damage = 10;
    
    private bool isInteracted = false;

    public override void Interact()
    {
        base.Interact();
    }

    public override void Open()
    {
        base.Open();

        // 폭팔 이펙트
        if(!isInteracted)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 0.8f);

            if (_player != null)
            {
                IDamageable damageable = _player.GetComponent<IDamageable>();
                damageable.TakeDamage(damage);
            }
        }

        isInteracted = true;
    }
}