using UnityEngine;

public class CampfireController : MonoBehaviour, IInteractable
{
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private Inventory playerInventory;

    private void Update()
    {
        if (CanInteract) fireParticles.Play();
        else fireParticles.Stop();
    }

    public bool CanInteract => playerInventory.HasItem(Item.Ruby);

    public void Interact(Inventory inventory)
    {
        if (!CanInteract) return;

        inventory.RemoveItem(Item.Ruby);
        
        // TODO: play whoosh sound
        // TODO: fade to black
    }
}
