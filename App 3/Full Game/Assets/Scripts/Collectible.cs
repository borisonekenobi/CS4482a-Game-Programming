using UnityEngine;

public abstract class Collectible : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip[] audioClips;
    protected abstract Item Item { get; }
    public bool CanInteract => true;

    public virtual void Interact(Inventory inventory)
    {
        AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length)], transform.position, 0.05f);
        inventory.AddItem(Item);
        Destroy(gameObject);
    }
}
