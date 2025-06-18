using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDeathPopup : BasePopup
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _MainMenuButton;
    
    public override UIType UIType => UIType.UIDeathPopup;
    
    
    
}
