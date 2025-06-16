using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteract
{
    protected GameObject _player;
    public bool IsInteractable { get; set; } = true;
    
    private void Awake()
    {
    }

    public virtual void Interact()
    {
        if (IsInteractable)
        {
            Debug.Log("NPC Interact");
        }

    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player = other.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player = null;
        }
    }
}