using System.Collections.Generic;
using UnityEngine;

namespace Aetherfall.Client.Controllers;

public sealed class InventoryController : MonoBehaviour
{
    private readonly List<string> _items = new();

    public IReadOnlyCollection<string> Items => _items;

    public void AddItem(string itemId)
    {
        if (!string.IsNullOrWhiteSpace(itemId))
        {
            _items.Add(itemId);
        }
    }
}
