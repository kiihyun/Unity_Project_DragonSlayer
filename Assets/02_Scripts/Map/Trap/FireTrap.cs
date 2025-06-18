using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : Trap
{
    private float _damage = Constants.Trap.FIRE_DAMAGE;
    private float _damageInterval = Constants.Trap.FIRE_DAMAGE_INTERVAL;
    private float _damageLastTime = 0f;

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

    public void OnFire()
    {
        if(_player != null)
        {
            _player.stat.TakeDamage(_damage);
        }
    }
}
