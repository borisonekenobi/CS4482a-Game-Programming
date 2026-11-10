using UnityEngine;
using UnityEngine.InputSystem;

public class JournalController : MonoBehaviour
{
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    [SerializeField] private Animator animator;
    [SerializeField] private InputAction toggleJournalAction;

    private void Start()
    {
        toggleJournalAction.Enable();
        toggleJournalAction.performed += _ => animator.SetBool(IsOpen, !animator.GetBool(IsOpen));
    }
}
