using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Beam : MonoBehaviour
{
    private float _damage = 1f;
    private float _damageInterval = 0.1f;
    private float _damageLastTime = 0f;

    private Player _player;

    private void Update()
    {
        if (_player != null)
        {
            _damageLastTime += Time.deltaTime;
            if(_damageLastTime >= _damageInterval)
            {
                _damageLastTime = 0f;
                print("Beam Damage" + _damage);
                _player.stat.TakeDamage(_damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            _player = player;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _player = null;
        }
    }
}
