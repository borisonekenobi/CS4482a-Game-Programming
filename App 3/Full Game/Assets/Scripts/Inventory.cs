using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Inventory : MonoBehaviour, IJournalPage
{
    private readonly Dictionary<Item, int> _items = new();

    public string Title => "Inventory";

    public string Summary
    {
        get
        {
            var summary = new StringBuilder();
            foreach (var kvp in _items) summary.AppendLine($"{ItemExtensions.ToString(kvp.Key)}: ×{kvp.Value}");
            return summary.ToString();
        }
    }

    public event Action<Item, int> ItemAdded;

    public void AddItem(Item item)
    {
        if (!_items.TryAdd(item, 1)) _items[item]++;

        ItemAdded?.Invoke(item, _items[item]);
    }

    public void RemoveItem(Item item)
    {
        if (!_items.ContainsKey(item)) return;

        _items[item]--;
        if (_items[item] <= 0) _items.Remove(item);
    }

    public int GetItemCount(Item item)
    {
        return _items.GetValueOrDefault(item, 0);
    }

    public int GetTotalItemCount()
    {
        return _items.Values.Sum();
    }

    public bool HasItem(Item item)
    {
        return GetItemCount(item) > 0;
    }

    public void SaveToGameData()
    {
        GameData.Inventory = new Dictionary<Item, int>(_items);
    }
}
