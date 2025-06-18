using UnityEngine;

public class Interaction : MonoBehaviour, IInteract
{

    public bool IsInteractable { get; set; } = true;
    public string InteractText = "E를 눌러 상호작용";

    public virtual string GetInteractText()
    {
        return InteractText;
    }

    protected GameObject _player;

    public virtual void Interact()
    {
        Debug.Log("Interact");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _player = other.gameObject;
            Player player = other.gameObject.GetComponent<Player>();
            PlayerController playerController = player.Controller;
            playerController.InteractObject = this;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _player = null;
            Player player = other.gameObject.GetComponent<Player>();
            PlayerController playerController = player.Controller;
            if(playerController.InteractObject == (IInteract)this)
            {
                playerController.InteractObject = null;
            }
        }
    }
}