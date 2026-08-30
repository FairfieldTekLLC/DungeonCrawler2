using Aetherfall.Domain.Common;

namespace Aetherfall.Domain.Crafting;

public static class CraftingFormulaService
{
    public static decimal CalculateQualityScore(int craftingSkill, decimal materialQuality, decimal specializationBonus, decimal stationQuality, decimal randomRoll)
    {
        if (craftingSkill < 0) throw new ArgumentOutOfRangeException(nameof(craftingSkill));
        ValidatePercentage(materialQuality, nameof(materialQuality));
        ValidatePercentage(specializationBonus, nameof(specializationBonus));
        ValidatePercentage(stationQuality, nameof(stationQuality));
        ValidatePercentage(randomRoll, nameof(randomRoll));

        return decimal.Round((craftingSkill * 0.40m) + (materialQuality * 0.25m) + (specializationBonus * 0.15m) + (stationQuality * 0.10m) + (randomRoll * 0.10m), 2);
    }

    public static Rarity ResolveRarity(decimal qualityScore)
        => qualityScore switch
        {
            < 30m => Rarity.Common,
            < 60m => Rarity.Uncommon,
            < 90m => Rarity.Rare,
            < 120m => Rarity.Epic,
            < 160m => Rarity.Legendary,
            _ => Rarity.Mythic
        };

    public static decimal CalculateCriticalChance(int skill)
    {
        if (skill < 0) throw new ArgumentOutOfRangeException(nameof(skill));
        return decimal.Round(Math.Min(0.20m, skill / 1000m + (skill / 3000m)), 4);
    }

    private static void ValidatePercentage(decimal value, string name)
    {
        if (value < 0 || value > 100)
        {
            throw new ArgumentOutOfRangeException(name, "Value must be between 0 and 100.");
        }
    }
}
