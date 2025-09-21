using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    None = 0,
    
    // Canvus_Fixed
    UIMainMenu,
    UIInGame,
    Joystick,
    
    // Canvus_Window
    MainWindow,
    UIPlayerStatus,
    UIEquipItem,
    UIInventory,
    UIOption,
    
    // Canvus_Popup
    UITooltip,
    UIInteraction,
    UIDeathPopup
}

public abstract class BaseUI : MonoBehaviour
{
    public abstract UIType UIType { get;}

    public virtual void OnOpen()
    {
        
    }

    public virtual void OnOpen(OpenParam openParam) => OnOpen(); 

    public virtual void OnClose()
    {
        
    }
}

public abstract class BaseFixed : BaseUI { }
public abstract class BaseWindow : BaseUI { }
public abstract class BasePopup : BaseUI { }
