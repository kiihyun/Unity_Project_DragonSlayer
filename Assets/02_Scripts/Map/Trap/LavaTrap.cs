using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrap : MonoBehaviour
{
    private float _damage = 1f;
    private float _damageInterval = 0.1f;
    private float _damageLastTime = 0f;

    private Player _player;

    public void Update()
    {
        if(_player != null)
        {
            _damageLastTime += Time.deltaTime;
            if(_damageLastTime >= _damageInterval)
            {
                _damageLastTime = 0f;
                _player.stat.TakeDamage(_damage);
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
