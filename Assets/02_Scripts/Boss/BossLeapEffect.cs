using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLeapEffect : MonoBehaviour
{
    public float Damage = 10f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.GetComponent<Player>()?.stat.TakeDamage(Damage);
            Destroy(this.gameObject);
        }
    }
}
