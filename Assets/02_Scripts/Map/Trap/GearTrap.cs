using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearTrap : MonoBehaviour
{
    private float _damage = 5f;

    private float _damageInterval = 0.5f;
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _player = collision.gameObject.GetComponent<Player>();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _player = null;
        }
    }
}
