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
        FadeManager.Instance.FadeOut(0.5f);
        yield return new WaitForSeconds(0.5f);
        _player.transform.position = new Vector3(OppositeDoor.transform.position.x, OppositeDoor.transform.position.y + 1, _player.transform.position.z);
        yield return new WaitForSeconds(0.5f);
        FadeManager.Instance.FadeIn(1f);
        yield return new WaitForSeconds(1f);
        IsInteractable = true;
    }
}