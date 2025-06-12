using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemySO")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private float _enemyID;
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackDelay;
}
