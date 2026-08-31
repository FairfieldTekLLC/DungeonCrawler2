using Aetherfall.Application.Abstractions;
using Aetherfall.Contracts.Crafting;
using Aetherfall.Domain.Crafting;
using Aetherfall.Domain.Inventory;

namespace Aetherfall.Application.Crafting;

public sealed record CraftItemCommand(Guid CharacterId, string RecipeId, decimal MaterialQuality, decimal SpecializationBonus, decimal StationQuality, decimal RandomRoll);

public sealed class CraftItemHandler : ICommandHandler<CraftItemCommand, CraftItemResponse>
{
    private readonly ICharacterRepository _characters;
    private readonly ICraftingRecipeRepository _recipes;

    public CraftItemHandler(ICharacterRepository characters, ICraftingRecipeRepository recipes)
    {
        _characters = characters;
        _recipes = recipes;
    }

    public async Task<Result<CraftItemResponse>> HandleAsync(CraftItemCommand command, CancellationToken cancellationToken)
    {
        var character = await _characters.GetByIdAsync(command.CharacterId, cancellationToken);
        if (character is null) return Result<CraftItemResponse>.Failure("Character not found.");

        var recipe = await _recipes.GetByIdAsync(command.RecipeId, cancellationToken);
        if (recipe is null) return Result<CraftItemResponse>.Failure("Recipe not found.");

        var qualityScore = CraftingFormulaService.CalculateQualityScore(
            recipe.RequiredSkill, 
            (double)command.MaterialQuality, 
            (int)command.SpecializationBonus, 
            (double)command.StationQuality, 
            (double)command.RandomRoll);
        
        var rarity = CraftingFormulaService.ResolveRarity(qualityScore);
        var critChance = CraftingFormulaService.CalculateCriticalChance(recipe.RequiredSkill);

        character.Inventory.AddItem(new InventoryItem(Guid.NewGuid(), recipe.ResultItemDefinitionId, Domain.Common.ItemCategory.Weapon, rarity, 1, Domain.Common.EquipmentSlot.MainHand));
        await _characters.UpdateAsync(character, cancellationToken);

        return Result<CraftItemResponse>.Success(new CraftItemResponse(recipe.ResultItemDefinitionId, rarity.ToString(), qualityScore, critChance));
    }
}
