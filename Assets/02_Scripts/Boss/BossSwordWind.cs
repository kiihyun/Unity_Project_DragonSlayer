using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwordWind : MonoBehaviour
{
    public float Damage = 10f;
    public float MoveSpeed = 5f;
    
    private Vector2 _dir;   // 이동 방향

    /// <summary>보스가 Instantiate 직후 호출</summary>
    public void Init(Vector3 casterPos, Vector3 targetPos)
    {
        _dir = (targetPos - casterPos).normalized;

        // 스프라이트가 앞으로 향하도록 z-회전 보정(2D 기준)
        float rotZ = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
    private void Update()
    {
        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.GetComponent<Player>()?.stat.TakeDamage(Damage);
            Destroy(this.gameObject);
        }
    }
}
