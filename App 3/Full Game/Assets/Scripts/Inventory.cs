using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private readonly Dictionary<string, int> _items = new();

    public void AddItem(string itemName)
    {
        if (!_items.TryAdd(itemName, 1))
        {
            _items[itemName]++;
        }
    }

    public string GetSummary()
    {
        var summary = new System.Text.StringBuilder();
        foreach (var kvp in _items)
        {
            summary.AppendLine($"{kvp.Key}: ×{kvp.Value}");
        }
        return summary.ToString();
    }
}
