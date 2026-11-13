public enum Item
{
    None,
    Daffodil,
    Hyacinth,
    Note,
    OrangeMushroom,
    PurpleMushroom,
    Ruby,
    Sunflower,
}

public static class ItemExtensions
{
    public static string ToString(this Item item)
    {
        return item switch
        {
            Item.None => null,
            Item.Daffodil => "Daffodil",
            Item.Hyacinth => "Hyacinth",
            Item.Note => "Note",
            Item.OrangeMushroom => "Orange Mushroom",
            Item.PurpleMushroom => "Purple Mushroom",
            Item.Ruby => "Ruby",
            Item.Sunflower => "Sunflower",
            _ => null
        };
    }
}
