using System.Collections.Generic;

public static class GameData
{
    public static Dictionary<Item, int> Inventory = new();
    public static List<ObjectiveDefinition> Objectives = new();
    public static int TotalObjectives;

    public static void Reset()
    {
        Inventory.Clear();
        Objectives.Clear();
        TotalObjectives = 0;
    }
}
