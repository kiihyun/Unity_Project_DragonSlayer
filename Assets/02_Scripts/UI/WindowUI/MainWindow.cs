using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : BaseWindow
{
    [SerializeField] private GameObject playerStatusPanel;
    [SerializeField] private GameObject equipItemPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject optionPanel;

    [SerializeField] private Button playerStatusTabButton;
    [SerializeField] private Button equipItemTabButton;
    [SerializeField] private Button inventoryTabButton;
    [SerializeField] private Button optionTabButton;

    public override UIType UIType => UIType.MainWindow;

    public void OnPlayerStatusTabButton()
    {
        UIManager.instance.SwitchWindowUI(UIType.UIPlayerStatus);
    }

    public void OnEquipItemTabButton()
    {
        UIManager.instance.SwitchWindowUI(UIType.UIEquipItem);
    }

    public void OnInventoryTabButton()
    {
        UIManager.instance.SwitchWindowUI(UIType.UIInventory);
    }

    public void OnOptionTabButton()
    {
        UIManager.instance.SwitchWindowUI(UIType.UIOption);
    }
    
}
