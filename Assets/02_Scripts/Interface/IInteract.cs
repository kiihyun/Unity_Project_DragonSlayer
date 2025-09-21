
public interface IInteract
{
    public bool IsInteractable { get; set; }

    public string GetInteractText();
    
    public void Interact();
}