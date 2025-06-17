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
    private PlayerStat _playerStat;
    
    private void Start()
    {
        _playerStat = UIManager.instance.player.GetComponent<PlayerStat>();
        if (_currentItem == null)
        {
            _currentItem.gameObject.SetActive(false);
        }

        if (_swapItem == null)
        {
            _swapItem.gameObject.SetActive(false);
        }
        
    }
    
    private void Update()
    {
        _healthBar.fillAmount = _playerStat.CurrentHealth / _playerStat.MaxHealth;
        _expBar.fillAmount = _playerStat.CurrentHealth / _playerStat.MaxHealth;
        _DashCoolDown.fillAmount = _playerStat.CurrentDashCooldown / _playerStat.DashCooldown;
        _skillCoolDown.fillAmount = _playerStat.CurrentSkillCooldown / _playerStat.SkillCooldown;
    }
    
    

}
