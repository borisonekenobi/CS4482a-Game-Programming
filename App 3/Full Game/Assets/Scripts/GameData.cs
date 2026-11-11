using System.Collections.Generic;

public static class GameData
{
    public static Dictionary<Item, int> Inventory = new();

    public static void Reset()
    {
        Inventory.Clear();
    }
}
