using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour , IDamageble
{
    [Header("Player Stats")]
    [SerializeField, Tooltip("플레이어 최대 체력")]
    private float _maxHealth;

    [SerializeField, Tooltip("현재 체력")]
    private float _currentHealth;

    [SerializeField, Range(1f, 20f), Tooltip("이동 속도")]
    private int _moveSpeed;

    [SerializeField, Tooltip("대시 힘")]
    private int _dashPower;

    [SerializeField, Tooltip("점프 힘")]
    private int _jumpPower;



    public float  MaxHealth => _maxHealth;

    public float CurrentHealth => _currentHealth;

    public float MoveSpeed { get { return _moveSpeed; } }

    public int DashPower { get { return _dashPower; } }

    public int JumpPower { get { return _jumpPower; } }


    private Player player;



    public void Init(Player player)
    {
        this.player = player;
        _currentHealth = _maxHealth;
    }


    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            player.Controller.IsDead();
        }

    }

    
}
