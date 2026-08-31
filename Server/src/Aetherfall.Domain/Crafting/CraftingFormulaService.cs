using System;

namespace Aetherfall.Domain.Crafting
{
    /// <summary>
    /// Handles all crafting math, quality scoring, and critical chance calculations.
    /// Implements the exact formulas from the 09-crafting-design document.
    /// </summary>
    public static class CraftingFormulaService
    {
        /// <summary>
        /// Calculates the Quality Score for a crafted item.
        /// Formula: QualityScore = CraftingSkill * 0.40 + MaterialQuality * 0.25 + SpecializationBonus * 0.15 + StationQuality * 0.10 + RandomRoll * 0.10
        /// </summary>
        public static double CalculateQualityScore(int craftingSkill, double materialQuality, int specializationBonus, double stationQuality, double randomRoll)
        {
            return (craftingSkill * 0.40)
                 + (materialQuality * 0.25)
                 + (specializationBonus * 0.15)
                 + (stationQuality * 0.10)
                 + (randomRoll * 0.10);
        }

        /// <summary>
        /// Calculates the critical crafting chance based on skill level using linear interpolation between documented breakpoints.
        /// Breakpoints: Skill 25 -> 1%, Skill 100 -> 5%, Skill 200 -> 10%, Skill 300 -> 20%
        /// </summary>
        public static double CalculateCriticalChance(int skillLevel)
        {
            if (skillLevel <= 25) return 0.01;
            if (skillLevel >= 300) return 0.20;

            // Interpolate linearly between known breakpoints
            if (skillLevel <= 100)
                return 0.01 + ((skillLevel - 25) / (100 - 25)) * (0.05 - 0.01);
            else if (skillLevel <= 200)
                return 0.05 + ((skillLevel - 100) / (200 - 100)) * (0.10 - 0.05);
            else
                return 0.10 + ((skillLevel - 200) / (300 - 200)) * (0.20 - 0.10);
        }

        /// <summary>
        /// Determines if a craft results in a critical success based on skill chance and a deterministic random roll.
        /// </summary>
        public static bool IsCriticalCraft(int skillLevel, double randomRoll)
        {
            return randomRoll <= CalculateCriticalChance(skillLevel);
        }

        /// <summary>
        /// Determines if a craft results in a critical success based on skill chance and a deterministic random roll.
        /// </summary>
        public static bool IsCriticalCraft(int skillLevel, decimal randomRoll) => IsCriticalCraft(skillLevel, (double)randomRoll);

        /// <summary>
        /// Maps a calculated quality score to an item rarity tier.
        /// Thresholds are representative of standard MMORPG progression scaling.
        /// </summary>
        public static string GetQualityTier(double qualityScore)
        {
            return qualityScore switch
            {
                >= 85 => "Mythic",
                >= 70 => "Legendary",
                >= 55 => "Epic",
                >= 40 => "Rare",
                >= 25 => "Uncommon",
                _ => "Common"
            };
        }

        /// <summary>
        /// Alias for GetQualityTier to resolve ambiguity in commands.
        /// </summary>
        public static string ResolveRarity(double qualityScore) => GetQualityTier(qualityScore);
    }
}
