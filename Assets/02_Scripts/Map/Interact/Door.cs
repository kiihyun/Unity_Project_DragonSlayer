using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    public GameObject OppositeDoor;

    public void Interact()
    {
        // TODO: 임시
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.transform.position = OppositeDoor.transform.position + new Vector3(0, 1, 0);
        }
    }

    [ContextMenu("Test")]
    public void Test()
    {
        StartCoroutine(Test2());
    }


    
    public IEnumerator Test2()
    {
        FadeManager.Instance.FadeOut(1f);
        yield return new WaitForSeconds(1f);
        Interact();
        FadeManager.Instance.FadeIn(1f);
        yield return new WaitForSeconds(1f);
    }
}