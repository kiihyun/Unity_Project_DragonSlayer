using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class StageLockDoor : Door
{
    [SerializeField] private GameObject _lockSprite;
    [SerializeField] private GameObject _unlockSprite;
    [SerializeField] private int _clearMaxStage;

    public override string GetInteractText()
    {
        if(StageManager.Instance.GetMaxClearStage() >= _clearMaxStage)
        {
            return "E를 눌러 상호작용";
        }
        else
        {
            return "아직 들어갈 이유가 없어";
        }
    }

    public override void Interact()
    {
        if(StageManager.Instance.GetMaxClearStage() < _clearMaxStage)
        {
            return;
        }

        if (_player != null && IsInteractable)
        {
            StartCoroutine(MoveToOppositeDoor());

        }
    }

}