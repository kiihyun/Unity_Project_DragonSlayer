using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrap : MonoBehaviour
{
    private float Damage = 1f;
    private float DamageInterval = 0.1f;
    private float DamageLastTime = 0f;

    private Player _player;

    public void Update()
    {
        if(_player != null)
        {
            DamageLastTime += Time.deltaTime;
            if(DamageLastTime >= DamageInterval)
            {
                DamageLastTime = 0f;
                _player.stat.TakeDamage(Damage);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _player = collision.gameObject.GetComponent<Player>();
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _player = null;
        }
    }
}
