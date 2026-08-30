using Aetherfall.Domain.Crafting;

namespace Aetherfall.Application.Abstractions;

public interface ICraftingRecipeRepository
{
    Task<CraftingRecipe?> GetByIdAsync(string recipeId, CancellationToken cancellationToken);
}
