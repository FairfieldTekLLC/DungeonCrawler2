using System;

namespace Aetherfall.Domain.Crafting
{
    /// <summary>
    /// Handles all crafting math, quality scoring, and critical chance calculations.
    /// Implements the exact formulas from the 09-crafting-design document.
    /// </summary>
    public static class CraftingFormulaService
    {
        // Quality score calculation weights
        private const double CraftingSkillWeight = 0.40;
        private const double MaterialQualityWeight = 0.25;
        private const double SpecializationBonusWeight = 0.15;
        private const double StationQualityWeight = 0.10;
        private const double RandomRollWeight = 0.10;

        // Critical chance breakpoints (skill level -> chance)
        private const int MinSkillLevel = 25;
        private const int SecondBreakpoint = 100;
        private const int ThirdBreakpoint = 200;
        private const int MaxSkillLevel = 300;

        private const double MinCritChance = 0.01;
        private const double SecondCritChance = 0.05;
        private const double ThirdCritChance = 0.10;
        private const double MaxCritChance = 0.20;

        // Quality tier thresholds
        private const double MythicThreshold = 85;
        private const double LegendaryThreshold = 70;
        private const double EpicThreshold = 55;
        private const double RareThreshold = 40;
        private const double UncommonThreshold = 25;

        // Quality tier names
        private const string MythicTier = "Mythic";
        private const string LegendaryTier = "Legendary";
        private const string EpicTier = "Epic";
        private const string RareTier = "Rare";
        private const string UncommonTier = "Uncommon";
        private const string CommonTier = "Common";

        /// <summary>
        /// Calculates the Quality Score for a crafted item.
        /// Formula: QualityScore = CraftingSkill * 0.40 + MaterialQuality * 0.25 + SpecializationBonus * 0.15 + StationQuality * 0.10 + RandomRoll * 0.10
        /// </summary>
        public static double CalculateQualityScore(int craftingSkill, double materialQuality, int specializationBonus, double stationQuality, double randomRoll)
        {
            return (craftingSkill * CraftingSkillWeight)
                 + (materialQuality * MaterialQualityWeight)
                 + (specializationBonus * SpecializationBonusWeight)
                 + (stationQuality * StationQualityWeight)
                 + (randomRoll * RandomRollWeight);
        }

        /// <summary>
        /// Calculates the critical crafting chance based on skill level using linear interpolation between documented breakpoints.
        /// Breakpoints: Skill 25 -> 1%, Skill 100 -> 5%, Skill 200 -> 10%, Skill 300 -> 20%
        /// </summary>
        public static double CalculateCriticalChance(int skillLevel)
        {
            if (skillLevel <= MinSkillLevel) return MinCritChance;
            if (skillLevel >= MaxSkillLevel) return MaxCritChance;

            // Interpolate linearly between known breakpoints
            if (skillLevel <= SecondBreakpoint)
                return MinCritChance + ((skillLevel - MinSkillLevel) / (double)(SecondBreakpoint - MinSkillLevel)) * (SecondCritChance - MinCritChance);
            else if (skillLevel <= ThirdBreakpoint)
                return SecondCritChance + ((skillLevel - SecondBreakpoint) / (double)(ThirdBreakpoint - SecondBreakpoint)) * (ThirdCritChance - SecondCritChance);
            else
                return ThirdCritChance + ((skillLevel - ThirdBreakpoint) / (double)(MaxSkillLevel - ThirdBreakpoint)) * (MaxCritChance - ThirdCritChance);
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
                >= MythicThreshold => MythicTier,
                >= LegendaryThreshold => LegendaryTier,
                >= EpicThreshold => EpicTier,
                >= RareThreshold => RareTier,
                >= UncommonThreshold => UncommonTier,
                _ => CommonTier
            };
        }

        /// <summary>
        /// Alias for GetQualityTier to resolve ambiguity in commands.
        /// </summary>
        public static string ResolveRarity(double qualityScore) => GetQualityTier(qualityScore);
    }
}
