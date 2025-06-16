using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    public GameObject OppositeDoor;

    public Player player;

    public bool IsInteractable { get; set; } = true;

    public virtual void Update()
    {
        // 임시
        if(Input.GetKeyDown(KeyCode.E) && player != null)
        {
            Interact();
        }
    }

    public virtual void Interact()
    {
        if (player != null && IsInteractable)
        {
            StartCoroutine(MoveToOppositeDoor()); // 조작 입력 필요
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            player = null;
        }
    }

    public virtual IEnumerator MoveToOppositeDoor()
    {
        IsInteractable = false;
        FadeManager.Instance.FadeOut(0.5f);
        yield return new WaitForSeconds(0.5f);
        player.transform.position = new Vector3(OppositeDoor.transform.position.x, OppositeDoor.transform.position.y + 1, player.transform.position.z);
        yield return new WaitForSeconds(0.5f);
        FadeManager.Instance.FadeIn(1f);
        yield return new WaitForSeconds(1f);
        IsInteractable = true;
    }
}