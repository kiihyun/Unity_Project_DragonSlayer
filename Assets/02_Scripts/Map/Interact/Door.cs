using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    public GameObject OppositeDoor;

    public Player player;

    public void Update()
    {
        // 임시
        if(Input.GetKeyDown(KeyCode.E) && player != null)
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (player != null)
        {
            player.transform.position = new Vector3(OppositeDoor.transform.position.x, OppositeDoor.transform.position.y + 1, player.transform.position.z);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            player = null;
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