using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DungeonSavePoint : SavePoint
{
    public override string GetInteractText()
    {
        return "저장하기";
    }
    public override void Interact()
    {
        SaveManager.Instance.SavePlayer();
    }
    [SerializeField] private float _moveDistance = 1f;
    [SerializeField] private float _duration = 1f;

    public void Start()
    {

    {
        // 현재 위치에서 Y축으로 moveDistance만큼 위로 이동, 그리고 다시 아래로 반복
        transform.DOLocalMoveY(transform.localPosition.y + _moveDistance, _duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
        }
    }
}
