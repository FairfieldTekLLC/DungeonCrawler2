namespace Aetherfall.Contracts.Crafting;

public sealed record CraftItemRequest(Guid CharacterId, string RecipeId, decimal MaterialQuality, decimal SpecializationBonus, decimal StationQuality, decimal RandomRoll);
public sealed record CraftItemResponse(string ItemDefinitionId, string Rarity, decimal QualityScore, decimal CriticalChance);
