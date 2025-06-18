using UnityEngine;

public class Interaction : MonoBehaviour, IInteract
{

    public bool IsInteractable { get; set; } = true;
    public string InteractText = Constants.Interaction.DEFAULT_INTERACT_TEXT;

    public virtual string GetInteractText()
    {
        return InteractText;
    }

    protected Player _player;

    public virtual void Interact()
    {
        Debug.Log("Interact");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _player = player;
            PlayerController playerController = player.Controller;
            playerController.InteractObject = this;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _player = null;
            PlayerController playerController = player.Controller;
            if(playerController.InteractObject == (IInteract)this)
            {
                playerController.InteractObject = null;
            }
        }
    }
}