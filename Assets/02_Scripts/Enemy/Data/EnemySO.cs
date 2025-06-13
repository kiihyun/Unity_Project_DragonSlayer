using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public float _enemyID;
    public int _health;
    public float _speed;
    public float _moveDelay;
    public float _attackRange;
    public int _attackDamage;
    public float _attackDelay;
}
