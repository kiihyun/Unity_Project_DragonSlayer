using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField, Tooltip("플레이어 최대 체력")]
    private int _maxHealth = 100;

    [SerializeField, Tooltip("현재 체력")]
    private int _currentHealth = 100;

    [SerializeField, Range(1f, 20f), Tooltip("이동 속도")]
    private int _moveSpeed = 5;

    [SerializeField, Tooltip("대시 힘")]
    private int _dashPower = 10;

    [SerializeField, Tooltip("점프 힘")]
    private int _jumpPower = 10;



    public int MaxHealth{ get { return _maxHealth; } }

    public int CurrentHealth { get { return _currentHealth; } }

    public float MoveSpeed { get { return _moveSpeed; } }

    public int DashPower { get { return _dashPower; } }

    public int JumpPower { get { return _jumpPower; } }

    public void StartStat()
    {
       _currentHealth = _maxHealth;

    }
}
