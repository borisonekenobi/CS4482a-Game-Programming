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

    private IInteractable _currentInteractable;

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
        _currentInteractable = null;

        var ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out var hit, pickupDistance))
        {
            _currentInteractable = hit.collider.GetComponentInParent<IInteractable>();
        }

        var canInteract = _currentInteractable != null;
        interactionPrompt.SetActive(canInteract);
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (_currentInteractable == null) return;

        _currentInteractable.Interact(inventory);

        _currentInteractable = null;
        interactionPrompt.SetActive(false);
    }
}
