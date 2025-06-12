using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIPath
{
    // UIPrefab 경로 저장
    private static readonly Dictionary<UIType, string> _path = new()
    {
        {UIType.MainMenu, "UI/MainMenu"},
        {UIType.UIMenu, "UI/UIMenu"},
        {UIType.UIPlayer, "UI/UIPlayer"},
        {UIType.UIInteraction, "UI/UIInteraction"},
        {UIType.Controller, "UI/Controller"}
    };
    
    // 해당 타입에 따른 경로 반환
    public static string GetPath(UIType type) => _path[type];
}
