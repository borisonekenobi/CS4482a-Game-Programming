using UnityEngine;

public abstract class Collectible : MonoBehaviour, IInteractable
{
    protected abstract Item Item { get; }
    public bool CanInteract => true;

    public virtual void Interact(Inventory inventory)
    {
        inventory.AddItem(Item);
        Destroy(gameObject);
    }
}
