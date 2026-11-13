using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Inventory : MonoBehaviour, IJournalPage
{
    private readonly Dictionary<Item, int> items = new();

    public string Title => "Inventory";

    public string Summary
    {
        get
        {
            var summary = new StringBuilder();
            foreach (var kvp in items) summary.AppendLine($"{ItemExtensions.ToString(kvp.Key)}: ×{kvp.Value}");
            return summary.ToString();
        }
    }

    public event Action<Item, int> ItemAdded;

    public void AddItem(Item item)
    {
        if (!items.TryAdd(item, 1)) items[item]++;

        ItemAdded?.Invoke(item, items[item]);
    }

    public void RemoveItem(Item item)
    {
        if (!items.ContainsKey(item)) return;

        items[item]--;
        if (items[item] <= 0) items.Remove(item);
    }

    public int GetItemCount(Item item)
    {
        return items.GetValueOrDefault(item, 0);
    }

    public int GetTotalItemCount()
    {
        return items.Values.Sum();
    }

    public bool HasItem(Item item)
    {
        return GetItemCount(item) > 0;
    }

    public void SaveToGameData()
    {
        GameData.Inventory = new Dictionary<Item, int>(items);
    }
}
