using Aetherfall.Domain.Common;

namespace Aetherfall.Domain.Crafting;

public sealed record CraftingIngredient
{
    public CraftingIngredient(string itemDefinitionId, int quantity)
    {
        ItemDefinitionId = Guard.AgainstNullOrWhiteSpace(itemDefinitionId, nameof(itemDefinitionId));
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        Quantity = quantity;
    }

    public string ItemDefinitionId { get; }
    public int Quantity { get; }
}

public sealed record CraftingRecipe
{
    public CraftingRecipe(string id, ProfessionType profession, int requiredSkill, string resultItemDefinitionId, IReadOnlyCollection<CraftingIngredient> ingredients)
    {
        Id = Guard.AgainstNullOrWhiteSpace(id, nameof(id));
        if (requiredSkill < 0) throw new ArgumentOutOfRangeException(nameof(requiredSkill));
        ResultItemDefinitionId = Guard.AgainstNullOrWhiteSpace(resultItemDefinitionId, nameof(resultItemDefinitionId));
        if (ingredients.Count == 0) throw new ArgumentException("Recipe requires ingredients.", nameof(ingredients));

        Profession = profession;
        RequiredSkill = requiredSkill;
        Ingredients = ingredients;
    }

    public string Id { get; }
    public ProfessionType Profession { get; }
    public int RequiredSkill { get; }
    public string ResultItemDefinitionId { get; }
    public IReadOnlyCollection<CraftingIngredient> Ingredients { get; }
}
