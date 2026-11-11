using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float pickupDistance = 3f;

    [Header("Inventory")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private JournalController journalController;

    [Header("Input")]
    [SerializeField] private InputAction interactAction;

    [Header("UI")]
    [SerializeField] private GameObject interactionPrompt;

    private ICollectible _currentCollectible;

    private void Update()
    {
        CheckForCollectible();
    }

    private void OnEnable()
    {
        interactAction.Enable();
        interactAction.performed += OnInteract;
    }

    private void OnDisable()
    {
        interactAction.performed -= OnInteract;
        interactAction.Disable();
    }

    private void CheckForCollectible()
    {
        _currentCollectible = null;

        var ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out var hit, pickupDistance))
        {
            _currentCollectible = hit.collider.GetComponentInParent<ICollectible>();
        }

        var canPickup = _currentCollectible != null;
        interactionPrompt.SetActive(canPickup);
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (_currentCollectible == null) return;

        var itemName = _currentCollectible.Name;

        inventory.AddItem(itemName);

        if (_currentCollectible is Note note) journalController.AddJournalPage(note);
        if (_currentCollectible is MonoBehaviour behaviour) Destroy(behaviour.gameObject);

        _currentCollectible = null;
        interactionPrompt.SetActive(false);
    }
}
