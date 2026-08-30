namespace Aetherfall.Contracts.Inventory;

public sealed record InventoryItemResponse(Guid ItemInstanceId, string DefinitionId, string Category, string Rarity, int Quantity, string Slot);
