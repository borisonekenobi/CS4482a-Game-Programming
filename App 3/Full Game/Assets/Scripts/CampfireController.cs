using UnityEngine;

public class CampfireController : MonoBehaviour, IInteractable
{
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private ObjectiveManager objectiveManager;

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
        inventory.SaveToGameData();
        objectiveManager.SaveToGameData();
        
        audioSource.Play();

        SceneFader.Instance.FadeToScene("Scenes/Finish");
    }
}
