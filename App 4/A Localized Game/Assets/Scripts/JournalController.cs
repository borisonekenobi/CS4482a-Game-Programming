using System;
using System.Collections.Generic;
using Borisonekenobi.Localization;
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

    private int journalIndex;
    private List<Tuple<string, Func<string>>> journalPages;
    private int MaxJournalPages => (journalPages.Count + 1) / 2 - 1;

    private void Start()
    {
        journalIndex = 0;
        journalPages = new List<Tuple<string, Func<string>>>
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
        if (nextPageAction.WasPressedThisFrame() && journalIndex < MaxJournalPages) journalIndex++;
        if (prevPageAction.WasPressedThisFrame() && journalIndex > 0) journalIndex--;

        var (leftJournalPageTitle, leftJournalPageContent) = journalPages[journalIndex * 2];
        var (rightJournalPageTitle, rightJournalPageContent) = journalIndex * 2 < journalPages.Count - 1
            ? journalPages[journalIndex * 2 + 1]
            : new Tuple<string, Func<string>>(string.Empty, () => string.Empty);

        leftPageTitle.SetText(Localization.Get(leftJournalPageTitle));
        leftPage.SetText(leftJournalPageContent());

        rightPageTitle.SetText(Localization.Get(rightJournalPageTitle));
        rightPage.SetText(rightJournalPageContent());
    }

    public void AddJournalPage(IJournalPage note)
    {
        journalPages.Add(new Tuple<string, Func<string>>(note.Title, () => note.Summary));
    }

    private void ToggleJournal(InputAction.CallbackContext context)
    {
        animator.SetBool(IsOpen, !animator.GetBool(IsOpen));
    }
}
