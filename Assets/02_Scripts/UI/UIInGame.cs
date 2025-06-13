using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인게임에서 보이는 UI
// 플레이어 체력, 골드, 현재 장착한 아이템, 환경설정 버튼
public abstract class UIInGame : BaseFixed
{
    public override UIType UIType => UIType.UIInGame;
}
