using UnityEngine;

public class Note : Collectible
{
    [SerializeField] private JournalController journalController;
    [SerializeField] public string noteTitle;
    [SerializeField] [TextArea] public string noteContent;
    protected override Item Item => Item.Note;

    public override void Interact(Inventory inventory)
    {
        inventory.AddItem(Item);
        journalController.AddJournalPage(this);
        Destroy(gameObject);
    }
}
