using Aetherfall.Domain.Common;

namespace Aetherfall.Domain.Inventory;

public sealed class InventoryItem
{
    public InventoryItem(Guid itemInstanceId, string definitionId, ItemCategory category, Rarity rarity, int quantity, EquipmentSlot slot = EquipmentSlot.None)
    {
        if (itemInstanceId == Guid.Empty) throw new ArgumentException("Item id cannot be empty.", nameof(itemInstanceId));
        DefinitionId = Guard.AgainstNullOrWhiteSpace(definitionId, nameof(definitionId));
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

        ItemInstanceId = itemInstanceId;
        Category = category;
        Rarity = rarity;
        Quantity = quantity;
        Slot = slot;
    }

    public Guid ItemInstanceId { get; }
    public string DefinitionId { get; }
    public ItemCategory Category { get; }
    public Rarity Rarity { get; }
    public EquipmentSlot Slot { get; }
    public int Quantity { get; private set; }

    public void AddQuantity(int amount)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Quantity += amount;
    }

    public void RemoveQuantity(int amount)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        if (Quantity < amount) throw new InvalidOperationException("Not enough quantity.");
        Quantity -= amount;
    }
}
