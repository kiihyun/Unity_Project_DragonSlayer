using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : BaseWindow
{
    [SerializeField] private Button playerStatusTabButton;
    [SerializeField] private Button equipItemTabButton;
    [SerializeField] private Button inventoryTabButton;
    [SerializeField] private Button optionTabButton;

    public override UIType UIType => UIType.MainWindow;

    public void OnPlayerStatusTabButton()
    {
        UIManager.instance.SwitchWindowUI(UIType.UIPlayerStatus, null, this.transform);
    }

    public void OnEquipItemTabButton()
    {
        UIManager.instance.SwitchWindowUI(UIType.UIEquipItem, null, this.transform);
    }

    public void OnInventoryTabButton()
    {
        UIManager.instance.SwitchWindowUI(UIType.UIInventory, null, this.transform);
    }

    public void OnOptionTabButton()
    {
        UIManager.instance.SwitchWindowUI(UIType.UIOption, null, this.transform);
    }

    public void OnCloseMainMenuButton()
    {
        UIManager.instance.OnCloseMainWindow();
    }
}
