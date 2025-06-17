using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergipaProjectile : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageble>(out IDamageble target))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy == null)
            {
                return;
            }

            target.TakeDamage(20);
            anim.CrossFade("Hit", 0.1f);
            rb.velocity = Vector2.zero; // Stop the projectile's movement
            Destroy(gameObject,1f);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Test"))
        {
            // Destroy the projectile when it hits a wall
            Destroy(gameObject);
        }

    }

}
