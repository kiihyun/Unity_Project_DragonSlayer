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
    private float _moveSpeed;

    [SerializeField, Tooltip("대시 힘")]
    private float _dashPower;

    [SerializeField, Tooltip("점프 힘")]
    private float _jumpPower;

    [SerializeField, Tooltip("플레이어의 공격력")]
    private int _attackPower;

    private int _level = 1; 

    private int _exp;

    public float  MaxHealth => _maxHealth;

    public float CurrentHealth => _currentHealth;

    public int EXP { get { return _exp; } }

    public int Level { get { return _level; } }

    public float MoveSpeed { get { return _moveSpeed; } }

    public float DashPower { get { return _dashPower; } }

    public float JumpPower { get { return _jumpPower; } }

    public float AttackPower { get { return _attackPower; } }

    public void Init()
    {
        _currentHealth = _maxHealth;
        _exp = 0;
    }

    public void TakeDamage(float damage)
    {
        Player player = GetComponent<Player>();
        StartCoroutine(player.Hit());
        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0);

        if (_currentHealth <= 0)
        {
            player.Controller.IsDead();
        }
    }

    public void LevelUP()
    {
            Debug.Log($"레벨업! 현재 레벨 : {_level}");
            _level++;
            _exp = 0;
            _attackPower += 2; // 레벨업 시 공격력 증가
            _maxHealth += 5; // 레벨업 시 최대 체력 증가
            _currentHealth = _maxHealth; // 최대 체력 증가 시 현재 체력도 회복
            _moveSpeed += 0.2f; // 레벨업 시 이동 속도 증가
    }

    public void GainExp(int exp)
    {
        _exp += exp;
        Debug.Log($"경험치 획득! 현재 경험치 : {_exp}");
        LevelUP();
    }


    
}
