using Borisonekenobi.Localization;

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
            Item.Daffodil => Localization.Get("item_daffodil"),
            Item.Hyacinth => Localization.Get("item_hyacinth"),
            Item.Note => Localization.Get("item_note"),
            Item.OrangeMushroom => Localization.Get("item_orange_mushroom"),
            Item.PurpleMushroom => Localization.Get("item_purple_mushroom"),
            Item.Ruby => Localization.Get("item_ruby"),
            Item.Sunflower => Localization.Get("item_sunflower"),
            _ => null
        };
    }
}
