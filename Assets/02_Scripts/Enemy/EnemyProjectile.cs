using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float _velocity;
    public bool IsLeft;
    private float _projectileTimer = 2f;
    public float CurTime;
    

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
        if(collision.TryGetComponent<IDamageable>(out var player))
        {
            player.TakeDamage(10);
        }

    }
}
