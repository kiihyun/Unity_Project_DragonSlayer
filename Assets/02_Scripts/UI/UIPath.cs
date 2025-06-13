using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIPath
{
    // UIPrefab 경로 저장
    private static readonly Dictionary<UIType, string> _path = new()
    {
        // Canvus_Fixed
        {UIType.UIMainMenu, "UI/UIMainMenu"},
        {UIType.Joystick, "UI/Joystick"},
        {UIType.UIInGame, "UI/UIInGame"},
        
        // Canvus_Window
        {UIType.UIPlayerStatus, "UI/UIPlayerStatus"},
        {UIType.UIEquipItem, "UI/UIEquipItem"},
        {UIType.UIInventory, "UI/UIInventory"},
        {UIType.UIOption, "UI/UIOption"},
        
        // Canvus_Popup
        {UIType.UITooltip, "UI/UITooltip"},
        {UIType.UIInteraction, "UI/UIInteraction"},
    };
    
    // 해당 타입에 따른 경로 반환
    public static string GetPath(UIType type) => _path[type];
}
