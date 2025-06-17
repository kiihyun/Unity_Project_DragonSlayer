using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatus : BaseWindow
{
    public override UIType UIType => UIType.UIPlayerStatus;
    
    [SerializeField] private TextMeshProUGUI _maxHealth;
    [SerializeField] private TextMeshProUGUI _currentHealth;
    [SerializeField] private TextMeshProUGUI _attackPower;
    [SerializeField] private TextMeshProUGUI _speed;
    
    private PlayerStat _playerStat;
    private Inventory _inventory;
    
    private void Start()
    {
        _playerStat = UIManager.instance.player.GetComponent<PlayerStat>();
        _inventory = UIManager.instance.inventory;
        
    }


    private void OnEnable()
    {
        if (_inventory != null)
        {
            _inventory.InventoryUpdate += UpdateUI;
        }

        UpdateUI();
    }

    private void OnDisable()
    {
        if (_inventory != null)
        {
            _inventory.InventoryUpdate -= UpdateUI;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_playerStat == null)
        {
            return;
        }
        
        float health = _playerStat.MaxHealth;
        float attackPower = _playerStat.AttackPower;
        float speed = _playerStat.MoveSpeed;

        foreach (var item in _inventory.EquipSlotItems)
        {
            if (item == null) continue;
            foreach (var stat in item.stats)
            {
                switch (stat.type)
                {
                    case StatType.Health:
                        health += stat.value;
                        break;
                    case StatType.AttackPower:
                        attackPower += stat.value;
                        break;
                    case StatType.Speed:
                        speed += stat.value;
                        break;
                }
            }
        }
        
        _maxHealth.text = health.ToString("F2");
        _currentHealth.text = _playerStat.CurrentHealth.ToString("F2");
        _attackPower.text = attackPower.ToString("F2");
        _speed.text = speed.ToString("F2");
        
    }
    
}
