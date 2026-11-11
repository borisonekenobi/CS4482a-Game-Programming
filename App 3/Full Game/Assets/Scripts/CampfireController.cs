using UnityEngine;

public class CampfireController : MonoBehaviour, IInteractable
{
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private AudioSource whooshSource;
    [SerializeField] private AudioSource fireSource;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private ObjectiveManager objectiveManager;

    private void Start()
    {
        if (CanInteract) return;

        fireSource.Stop();
        fireParticles.Stop();

        playerInventory.ItemAdded += (item, _) =>
        {
            if (item != Item.Ruby) return;

            fireSource.Play();
            fireParticles.Play();
        };
    }

    public bool CanInteract => playerInventory.HasItem(Item.Ruby);

    public void Interact(Inventory inventory)
    {
        if (!CanInteract) return;

        inventory.RemoveItem(Item.Ruby);
        inventory.SaveToGameData();
        objectiveManager.SaveToGameData();

        whooshSource.Play();

        SceneFader.Instance.FadeToScene("Scenes/Finish");
    }
}
