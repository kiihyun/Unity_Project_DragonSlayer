using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 인게임에서 보이는 UI
// 플레이어 체력, 골드, 현재 장착한 아이템, 환경설정 버튼
public class UIInGame : BaseFixed
{
    public override UIType UIType => UIType.UIInGame;
    
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _DashCoolDown;
    [SerializeField] private Image _skillCoolDown;
    [SerializeField] private Image _expBar;
    [SerializeField] private Image _currentItem;
    [SerializeField] private Image _swapItem;
    [SerializeField]private PlayerStat _playerStat;
    [SerializeField]private Inventory _inventory;
    
    private void Start()
    {
        _playerStat = UIManager.instance.player.GetComponent<PlayerStat>();
        _inventory = UIManager.instance.inventory;
        _inventory.InventoryUpdate += UpdateItemUI;
        _currentItem.gameObject.SetActive(false);
        _swapItem.gameObject.SetActive(false);
        UpdateItemUI();
    }
    
    private void Update()
    {
        _healthBar.fillAmount = _playerStat.CurrentHealth / _playerStat.MaxHealth;
        _expBar.fillAmount = _playerStat.CurrentHealth / _playerStat.MaxHealth;
        _DashCoolDown.fillAmount = _playerStat.CurrentDashCooldown / _playerStat.DashCooldown;
        _skillCoolDown.fillAmount = _playerStat.CurrentSkillCooldown / _playerStat.SkillCooldown;
        
    }
    

    private void UpdateItemUI()
    {
        // 0번 슬롯: 현재 아이템
        if (_inventory.QuickSlotItems[0] != null)
        {
            _currentItem.sprite = _inventory.QuickSlotItems[0].ItemIcon;
            _currentItem.gameObject.SetActive(true);
        }
        else
        {
            _currentItem.gameObject.SetActive(false);
        }

        // 1번 슬롯: 교체 예비 아이템
        if (_inventory.QuickSlotItems[1] != null)
        {
            _swapItem.sprite = _inventory.QuickSlotItems[1].ItemIcon;
            _swapItem.gameObject.SetActive(true);
        }
        else
        {
            _swapItem.gameObject.SetActive(false);
        }
    }
    
    

}
