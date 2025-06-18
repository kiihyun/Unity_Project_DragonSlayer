using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public float EnemyID;
    public int Health;
    public float Speed;
    public float MoveDelay;
    public float AttackRange;
    public int AttackDamage;
    public bool Guardable;
    public bool RangeAttackable;
    public int Experience;
}
