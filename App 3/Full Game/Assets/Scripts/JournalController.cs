using System;
using System.Collections.Generic;
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
    [SerializeField] private InputAction nextPageAction;
    [SerializeField] private InputAction prevPageAction;

    private int _journalIndex;
    private List<Tuple<string, Func<string>>> _journalPages;
    private int MaxJournalPages => (_journalPages.Count + 1) / 2 - 1;

    private void Start()
    {
        _journalIndex = 0;
        _journalPages = new List<Tuple<string, Func<string>>>
        {
            new(inventory.Title, () => inventory.Summary),
            new(objectiveManager.Title, () => objectiveManager.Summary)
        };
    }

    private void OnEnable()
    {
        toggleJournalAction.Enable();
        toggleJournalAction.performed += ToggleJournal;
        nextPageAction.Enable();
        prevPageAction.Enable();
    }

    private void OnDisable()
    {
        toggleJournalAction.performed -= ToggleJournal;
        toggleJournalAction.Disable();
        nextPageAction.Disable();
        prevPageAction.Disable();
    }

    private void Update()
    {
        if (nextPageAction.WasPressedThisFrame() && _journalIndex < MaxJournalPages) _journalIndex++;
        if (prevPageAction.WasPressedThisFrame() && _journalIndex > 0) _journalIndex--;

        var (leftJournalPageTitle, leftJournalPageContent) = _journalPages[_journalIndex * 2];
        var (rightJournalPageTitle, rightJournalPageContent) = _journalIndex * 2 < _journalPages.Count - 1
            ? _journalPages[_journalIndex * 2 + 1]
            : new Tuple<string, Func<string>>(string.Empty, () => string.Empty);

        leftPageTitle.SetText(leftJournalPageTitle);
        leftPage.SetText(leftJournalPageContent());

        rightPageTitle.SetText(rightJournalPageTitle);
        rightPage.SetText(rightJournalPageContent());
    }

    public void AddJournalPage(Note note)
    {
        _journalPages.Add(new Tuple<string, Func<string>>(note.noteTitle, () => note.noteContent));
    }

    private void ToggleJournal(InputAction.CallbackContext context)
    {
        animator.SetBool(IsOpen, !animator.GetBool(IsOpen));
    }
}
