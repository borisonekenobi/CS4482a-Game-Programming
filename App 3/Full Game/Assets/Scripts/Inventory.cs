using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Inventory : MonoBehaviour, IJournalPage
{
    private readonly Dictionary<string, int> _items = new();

    public string Title => "Inventory";

    public string Summary
    {
        get
        {
            var summary = new StringBuilder();
            foreach (var kvp in _items) summary.AppendLine($"{kvp.Key}: ×{kvp.Value}");
            return summary.ToString();
        }
    }

    public event Action<string, int> ItemAdded;

    public void AddItem(string itemName)
    {
        if (!_items.TryAdd(itemName, 1)) _items[itemName]++;

        ItemAdded?.Invoke(itemName, _items[itemName]);
    }

    public int GetItemCount(string itemName)
    {
        return _items.GetValueOrDefault(itemName, 0);
    }

    public int GetTotalItemCount()
    {
        return _items.Values.Sum();
    }
}
