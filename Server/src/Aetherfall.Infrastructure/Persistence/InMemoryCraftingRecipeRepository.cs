using Aetherfall.Application.Abstractions;
using Aetherfall.Domain.Common;
using Aetherfall.Domain.Crafting;

namespace Aetherfall.Infrastructure.Persistence;

public sealed class InMemoryCraftingRecipeRepository : ICraftingRecipeRepository
{
    private static readonly IReadOnlyDictionary<string, CraftingRecipe> Recipes = new Dictionary<string, CraftingRecipe>
    {
        ["recipe.ironblade"] = new("recipe.ironblade", ProfessionType.Blacksmithing, 40, "item.ironblade", new[]
        {
            new CraftingIngredient("item.ironore", 3),
            new CraftingIngredient("item.ashwood", 1)
        })
    };

    public Task<CraftingRecipe?> GetByIdAsync(string recipeId, CancellationToken cancellationToken)
    {
        Recipes.TryGetValue(recipeId, out var recipe);
        return Task.FromResult(recipe);
    }
}
