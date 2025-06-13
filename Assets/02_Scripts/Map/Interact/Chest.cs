using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject closedSprite;
    [SerializeField] private GameObject openedSprite;
    private bool isOpen = false;
    protected GameObject _player;

    private void Awake()
    {
        // 시작 시 닫힌 상태로 초기화
        SetSpriteState(isOpen);
    }

    public virtual void Interact()
    {
        if (isOpen)
            Close();
        else
            Open();
    }

    public virtual void Open()
    {
        isOpen = true;
        SetSpriteState(isOpen);
        Debug.Log("Open");
    }

    public virtual void Close()
    {
        isOpen = false;
        SetSpriteState(isOpen);
        Debug.Log("Close");
    }

    private void SetSpriteState(bool open)
    {
        closedSprite.SetActive(!open);
        openedSprite.SetActive(open);
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

    [ContextMenu("Test")]
    public void Test()
    {
        Interact();
    }
}