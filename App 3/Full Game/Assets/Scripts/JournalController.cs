using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class JournalController : MonoBehaviour
{
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    [Header("References")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private ObjectiveManager objectiveManager;
    [SerializeField] private Animator animator;
    [SerializeField] private InputAction toggleJournalAction;

    [Header("Pages")]
    [SerializeField] private TMP_Text leftPageTitle;
    [SerializeField] private TMP_Text leftPage;
    [SerializeField] private TMP_Text rightPageTitle;
    [SerializeField] private TMP_Text rightPage;

    private void OnEnable()
    {
        toggleJournalAction.Enable();
        toggleJournalAction.performed += ToggleJournal;
    }

    private void OnDisable()
    {
        toggleJournalAction.performed -= ToggleJournal;
        toggleJournalAction.Disable();
    }

    private void Update()
    {
        leftPageTitle.SetText(inventory.Title);
        leftPage.SetText(inventory.Summary);

        rightPageTitle.SetText(objectiveManager.Title);
        rightPage.SetText(objectiveManager.Summary);
    }

    private void ToggleJournal(InputAction.CallbackContext context)
    {
        animator.SetBool(IsOpen, !animator.GetBool(IsOpen));
    }
}
