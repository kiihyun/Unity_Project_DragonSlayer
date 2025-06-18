using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class StageLockDoor : Door
{
    [SerializeField] private int _clearMaxStage;

    public override string GetInteractText()
    {
        if(StageManager.Instance.GetMaxClearStage() >= _clearMaxStage)
        {
            return Constants.Interaction.DEFAULT_INTERACT_TEXT;
        }
        else
        {
            return Constants.Interaction.STAGE_LOCK_DOOR_INTERACT_TEXT;
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