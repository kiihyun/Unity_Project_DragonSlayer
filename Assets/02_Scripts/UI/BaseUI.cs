using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    None = 0,
    MainMenu,
    UIMenu,
    UIPlayer,
    UIInteraction,
    Controller
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
