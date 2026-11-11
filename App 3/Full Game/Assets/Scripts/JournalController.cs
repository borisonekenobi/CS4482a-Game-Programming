using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class JournalController : MonoBehaviour
{
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    [Header("References")]
    [SerializeField] private Inventory inventory;
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
        leftPageTitle.SetText("Inventory");
        leftPage.SetText(inventory.GetSummary());

        rightPageTitle.SetText("Objectives");
        rightPage.SetText("");
    }

    private void ToggleJournal(InputAction.CallbackContext context)
    {
        animator.SetBool(IsOpen, !animator.GetBool(IsOpen));
    }
}
