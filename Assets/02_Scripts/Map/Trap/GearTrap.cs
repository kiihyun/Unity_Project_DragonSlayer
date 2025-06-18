using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearTrap : Trap
{
    private float _damage = 5;
    private float _damageInterval = Constants.Trap.GEAR_DAMAGE_INTERVAL;
    private float _damageLastTime = 0f;

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
}
