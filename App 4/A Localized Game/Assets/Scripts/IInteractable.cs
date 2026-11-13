public interface IInteractable
{
    public bool CanInteract { get; }
    public void Interact(Inventory inventory);
}
