using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour , IDamageble
{
    [Header("Player Stats")]
    [SerializeField, Tooltip("ï¿½Ã·ï¿½ï¿½Ì¾ï¿½ ï¿½Ö´ï¿½ Ã¼ï¿½ï¿½")]
    private float _maxHealth;

    [SerializeField, Tooltip("ï¿½ï¿½ï¿½ï¿½ Ã¼ï¿½ï¿½")]
    private float _currentHealth;

    [SerializeField, Range(1f, 20f), Tooltip("ï¿½Ìµï¿½ ï¿½Óµï¿½")]
    private float _moveSpeed;

    [SerializeField, Tooltip("ï¿½ï¿½ï¿?ï¿½ï¿½")]
    private float _dashPower;

    [SerializeField, Tooltip("ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½")]
    private float _jumpPower;

    [SerializeField, Tooltip("ï¿½Ã·ï¿½ï¿½Ì¾ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½Ý·ï¿½")]
    private int _attackPower;

    [SerializeField, Tooltip("´ë½Ã ÄðÅ¸ÀÓ (ÃÊ ´ÜÀ§)")]
    private float dashCooldown = 2f; // ´ë½Ã ÄðÅ¸ÀÓ

    [SerializeField, Tooltip("½ºÅ³ ÄðÅ¸ÀÓ (ÃÊ ´ÜÀ§)")]
    private float skillCooldown = 5f; // ½ºÅ³ ÄðÅ¸ÀÓ (ÃÊ ´ÜÀ§)

    private int _level = 1; 

    private int _exp;

    public float  MaxHealth => _maxHealth;

    public float CurrentHealth => _currentHealth;


    public float DashCooldown { get { return dashCooldown; } }

    private float currentDashCooldown = 0f; // ÇöÀç ´ë½Ã ÄðÅ¸ÀÓ

    public float CurrentDashCooldown
    {
        get { return currentDashCooldown; }
        set { currentDashCooldown = value; }
    }

    public float SkillCooldown { get { return skillCooldown; } }

    private float currentSkillCooldown = 0f; // ÇöÀç ½ºÅ³ ÄðÅ¸ÀÓ

    public float CurrentSkillCooldown
    {
        get { return currentSkillCooldown; }
        set { currentSkillCooldown = value; }
    }


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
            Debug.Log($"ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½! ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ : {_level}");
            _level++;
            _exp = 0;
            _attackPower += 2; // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½Ý·ï¿½ ï¿½ï¿½ï¿½ï¿½
            _maxHealth += 5; // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½Ö´ï¿½ Ã¼ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
            _currentHealth = _maxHealth; // ï¿½Ö´ï¿½ Ã¼ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ Ã¼ï¿½Âµï¿½ È¸ï¿½ï¿½
            _moveSpeed += 0.2f; // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½Ìµï¿½ ï¿½Óµï¿½ ï¿½ï¿½ï¿½ï¿½
    }

    public void GainExp(int exp)
    {
        _exp += exp;
        Debug.Log($"ï¿½ï¿½ï¿½ï¿½Ä¡ È¹ï¿½ï¿½! ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½Ä¡ : {_exp}");
        LevelUP();
    }

    public void UpdateStats(StatType type, int value)
    {
        switch (type)
        {
            case StatType.Health:
                _maxHealth += value;
                _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
                break;
            case StatType.AttackPower:
                _attackPower += value;
                break;
            case StatType.Speed:
                _moveSpeed += value;
                break;
            default:
                Debug.LogError($"ì¡´ìž¬?˜ì? ?ŠëŠ” ?€?? {type}");
                break;
        }
    }

    
}
