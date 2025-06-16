using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Beam : MonoBehaviour
{
    float Damage = 1f;
    float DamageInterval = 1f;
    float DamageLastTime = 0f;

    Player _player;

    private void Init(float damage, float damageInterval)
    {
        Damage = damage;
        DamageInterval = damageInterval;
    }

    private void Update()
    {
        if (_player != null)
        {
            DamageLastTime += Time.deltaTime;
            if(DamageLastTime >= DamageInterval)
            {
                DamageLastTime = 0f;
                print("Beam Damage" + Damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        // if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            _player = player;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        // if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _player = null;
        }
    }
}
