using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerStatus : BaseWindow
{
    public override UIType UIType => UIType.UIPlayerStatus;
    
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _strength;
    [SerializeField] private TextMeshProUGUI _speed;
}
