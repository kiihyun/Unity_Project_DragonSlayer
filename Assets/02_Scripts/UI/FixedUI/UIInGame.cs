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
    private PlayerStat _playerStat;
    
    private void Start()
    {
        _playerStat = UIManager.instance.player.GetComponent<PlayerStat>();
        
    }
    
    private void Update()
    {
        _healthBar.fillAmount = _playerStat.CurrentHealth / _playerStat.MaxHealth;
    }

}
