using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DungeonSavePoint : SavePoint
{
    [SerializeField] private float _moveDistance = 1f;
    [SerializeField] private float _duration = 1f;

    private SaveManager _saveManager;
    public void Start()
    {
        _saveManager = SaveManager.Instance;

        // 현재 위치에서 Y축으로 moveDistance만큼 위로 이동, 그리고 다시 아래로 반복
        transform.DOLocalMoveY(transform.localPosition.y + _moveDistance, _duration)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.InOutSine);
    }
    public override string GetInteractText()
    {
        return Constants.Interaction.SAVE_POINT_INTERACT_TEXT;
    }
    public override void Interact()
    {
        if(_saveManager == null)
        {
            _saveManager = SaveManager.Instance;
        }

        _saveManager.SavePlayer();
    }
}
