using UnityEngine;

public class BossBreathHitbox : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.GetComponent<Player>()?.stat.TakeDamage(_damage);
        }

    }
}