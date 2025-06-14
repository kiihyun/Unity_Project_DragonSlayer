using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageble
{
    public float MaxHealth { get; }

    public float CurrentHealth { get; }

    public void TakeDamage(float damage);
}
