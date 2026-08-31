using Aetherfall.Domain.Common;
using Aetherfall.Domain.Crafting;

namespace Aetherfall.Domain.Tests.Crafting;

public sealed class CraftingFormulaServiceTests
{
    [Fact]
    public void CalculateQualityScore_MapsToExpectedRarity()
    {
        var score = CraftingFormulaService.CalculateQualityScore(120, 80, 60, 50, 40);
        var rarity = CraftingFormulaService.ResolveRarity(score);

        Assert.Equal(86.0, score, precision: 1);
        Assert.Equal("Mythic", rarity);
    }
}
