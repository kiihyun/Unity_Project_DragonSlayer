using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Beam : Trap
{
    private float _damage = Constants.Trap.BEAM_DAMAGE;
    private float _damageInterval = Constants.Trap.BEAM_DAMAGE_INTERVAL;
    private float _damageLastTime = 0f;

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
}
