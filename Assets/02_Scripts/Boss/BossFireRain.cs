using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireRain : MonoBehaviour
{
    public float fallSpeed = 10f;
    private float _damage;

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
        if (other.CompareTag("Player"))
        {
            // other.GetComponent<Player>()?.TakeDamage(damage);
            Debug.Log("�÷��̾ �ҵ��̿� ����!");
            Destroy(gameObject); // �浹 �� ����
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Test"))
        {
            Destroy(gameObject);
        }
    }
}