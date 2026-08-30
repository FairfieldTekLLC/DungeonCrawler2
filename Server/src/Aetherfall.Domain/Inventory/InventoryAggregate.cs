using Aetherfall.Domain.Abstractions;
using Aetherfall.Domain.Common;

namespace Aetherfall.Domain.Inventory;

public sealed class InventoryAggregate : Entity
{
    private readonly List<InventoryItem> _items = new();

    public InventoryAggregate(Guid id, decimal weightCapacity) : base(id)
    {
        WeightCapacity = Guard.AgainstNegative(weightCapacity, nameof(weightCapacity));
    }

    public decimal WeightCapacity { get; }
    public IReadOnlyCollection<InventoryItem> Items => _items.AsReadOnly();

    public void AddItem(InventoryItem item)
    {
        ArgumentNullException.ThrowIfNull(item);
        var existing = _items.FirstOrDefault(x => x.DefinitionId == item.DefinitionId && x.Rarity == item.Rarity && x.Slot == item.Slot);
        if (existing is null)
        {
            _items.Add(item);
        }
        else
        {
            existing.AddQuantity(item.Quantity);
        }
    }

    public void RemoveItem(Guid itemInstanceId, int quantity)
    {
        var item = _items.FirstOrDefault(x => x.ItemInstanceId == itemInstanceId) ?? throw new InvalidOperationException("Item not found.");
        item.RemoveQuantity(quantity);
        if (item.Quantity == 0)
        {
            _items.Remove(item);
        }
    }
}
