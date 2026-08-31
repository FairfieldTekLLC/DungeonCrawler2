using Aetherfall.Domain.Characters;

namespace Aetherfall.Domain.Tests.Characters;

public sealed class CharacterFormulaServiceTests
{
    [Fact]
    public void Calculate_UsesDocumentedFormulas()
    {
        var attributes = new CharacterAttributes(20, 15, 12, 18, 10, 8);

        var stats = CharacterFormulaService.Calculate(5, attributes, 25m, 30m, 0.05m);

        Assert.Equal(406m, stats.MaxHealth);
        Assert.Equal(226m, stats.MaxMana);
        Assert.Equal(240m, stats.MaxStamina);
        Assert.Equal(28.75m, stats.PhysicalDamage);
        Assert.Equal(33.12m, stats.SpellDamage);
        Assert.Equal(0.0668m, stats.CriticalChance);
    }

    [Fact]
    public void ApplyAttributeCaps_ReducesGrowthPast250()
    {
        var result = CharacterFormulaService.ApplyAttributeCaps(300);
        Assert.Equal(275, result);
    }
}
