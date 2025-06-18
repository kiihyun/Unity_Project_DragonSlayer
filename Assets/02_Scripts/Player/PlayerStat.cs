using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerStat : MonoBehaviour, IDamageble
{
    [Header("Player Stats")]
    [SerializeField, Tooltip("�÷��̾� �ִ� ü��")]
    private float _maxHealth;

    [SerializeField, Tooltip("���� ü��")]
    private float _currentHealth;

    [SerializeField, Range(1f, 20f), Tooltip("�̵� �ӵ�")]
    private float _moveSpeed;

    [SerializeField, Tooltip("���?��")]
    private float _dashPower;

    [SerializeField, Tooltip("���� ��")]
    private float _jumpPower;

    [SerializeField, Tooltip("�÷��̾��� ���ݷ�")]
    private int _attackPower;

    [SerializeField, Tooltip("��� ��Ÿ�� (�� ����)")]
    private float dashCooldown = 2f; // ��� ��Ÿ��

    [SerializeField, Tooltip("��ų ��Ÿ�� (�� ����)")]
    private float skillCooldown = 5f; // ��ų ��Ÿ�� (�� ����)

    public int _maxLevel = 10; // �ִ� ���� ���� (��: 10�������� ����)

    private int _level = 1;
    public int MaxExp {get {return _maxExp;} }

    private int _maxExp; 

    private int _currentexp;



    public float MaxHealth => _maxHealth;

    public float CurrentHealth => _currentHealth;


    public float DashCooldown { get { return dashCooldown; } }

    private float currentDashCooldown = 0f; // ���� ��� ��Ÿ��

    public float CurrentDashCooldown
    {
        get { return currentDashCooldown; }
        set { currentDashCooldown = value; }
    }

    public float SkillCooldown { get { return skillCooldown; } }

    private float currentSkillCooldown = 0f; // ���� ��ų ��Ÿ��

    public float CurrentSkillCooldown
    {
        get { return currentSkillCooldown; }
        set { currentSkillCooldown = value; }
    }


    public int CurrentEXP { get { return _currentexp; } }

    public int Level { get { return _level; } }

    public float MoveSpeed { get { return _moveSpeed; } }

    public float DashPower { get { return _dashPower; } }

    public float JumpPower { get { return _jumpPower; } }

    public float AttackPower { get { return _attackPower; } }

    public PlayerStat(float maxHealth, float currentHealth, float moveSpeed,
                      float dashPower, float jumpPower, int attackPower,
                      int level, int exp)
    {
        _maxHealth = maxHealth;
        _currentHealth = currentHealth;
        _moveSpeed = moveSpeed;
        _dashPower = dashPower;
        _jumpPower = jumpPower;
        _attackPower = attackPower;
        _level = level;
        _currentexp = exp;
    }

    public void Init()
    {
        _currentHealth = _maxHealth;
        _currentexp = 0;
        _level = 1;
        _maxExp = 10;
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
        if (_maxLevel <= _currentexp)
        {
            _level++;
            _currentexp = 0;
            _attackPower += 2; // ������ �� ���ݷ� ����
            _maxHealth += 5; // ������ �� �ִ� ü�� ����
            _currentHealth = _maxHealth; // �ִ� ü�� ���� �� ���� ü�µ� ȸ��
            _moveSpeed += 0.2f; // ������ �� �̵� �ӵ� ����
        }
    }

    public void GainExp(int exp)
    {
        _currentexp += exp;
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
                //Debug.LogError($"존재?��? ?�는 ?�?? {type}");
                break;
        }
    }


}
