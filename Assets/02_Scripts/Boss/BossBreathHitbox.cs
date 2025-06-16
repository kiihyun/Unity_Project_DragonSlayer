using UnityEngine;

public class BossBreathHitbox : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                //player.TakeDamage(damage);
                Debug.Log("브레스에 맞음! 피해 입힘");
            }
        }
    }
}