using Borisonekenobi.Localization;
using UnityEngine;

public class Note : Collectible, IJournalPage
{
    [SerializeField] private JournalController journalController;
    [SerializeField] public string noteTitle;
    [SerializeField] public string noteContent;
    protected override Item Item => Item.Note;

    public string Title => noteTitle;
    public string Summary => Localization.Get(noteContent);

    public override void Interact(Inventory inventory)
    {
        inventory.AddItem(Item);
        journalController.AddJournalPage(this);
        Destroy(gameObject);
    }
}
