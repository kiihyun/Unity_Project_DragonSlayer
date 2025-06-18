using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interaction
{
    public GameObject OppositeDoor;

    public override void Interact()
    {
        if (_player != null && IsInteractable)
        {
            StartCoroutine(MoveToOppositeDoor()); // 조작 입력 필요
        }
    }

    public virtual IEnumerator MoveToOppositeDoor()
    {
        IsInteractable = false;
        FadeManager.Instance.FadeOut(Constants.Interaction.DOOR_FADEOUT_DURATION);
        yield return new WaitForSeconds(Constants.Interaction.DOOR_FADEOUT_DURATION);
        _player.transform.position = new Vector3(OppositeDoor.transform.position.x, OppositeDoor.transform.position.y + 1, _player.transform.position.z);
        yield return new WaitForSeconds(Constants.Interaction.DOOR_MOVE_DURATION);
        FadeManager.Instance.FadeIn(Constants.Interaction.DOOR_FADEIN_DURATION);
        yield return new WaitForSeconds(Constants.Interaction.DOOR_FADEIN_DURATION);
        IsInteractable = true;
    }
}