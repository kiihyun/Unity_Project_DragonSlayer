using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    private float _damage = 10f;

    private float _damageInterval = 2f;
    private float _damageLastTime = 0f;

    private Player _player;

    private Animator _animator;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void Update()
    {
        _damageLastTime += Time.deltaTime;
        if(_damageLastTime >= _damageInterval)
        {
            _animator.SetTrigger("IsFire");
            _damageLastTime = 0f;
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

    public void OnFire()
    {
        if(_player != null)
        {
            _player.stat.TakeDamage(_damage);
        }
    }
}
