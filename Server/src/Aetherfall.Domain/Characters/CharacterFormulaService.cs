using System;

namespace Aetherfall.Domain.Characters
{
    /// <summary>
    /// Service providing character stat calculation formulas.
    /// All methods use documented game balance formulas for health, mana, stamina, damage, and critical chance calculations.
    /// </summary>
    public static class CharacterFormulaService
    {
        // Health calculation constants
        private const int BaseHealth = 100;
        private const int HealthPerLevel = 18;
        private const int HealthPerVitality = 12;

        // Mana calculation constants
        private const int BaseMana = 50;
        private const int ManaPerLevel = 8;
        private const int ManaPerIntelligence = 8;
        private const int ManaPerWisdom = 4;

        // Stamina calculation constants
        private const int BaseStamina = 75;
        private const int StaminaPerLevel = 6;
        private const int StaminaPerDexterity = 3;
        private const int StaminaPerVitality = 5;

        // Damage calculation coefficients
        private const double StrengthDamageCoefficient = 0.006;
        private const double DexterityPhysicalDamageCoefficient = 0.002;
        private const double IntelligenceDamageCoefficient = 0.007;
        private const double WisdomDamageCoefficient = 0.002;

        // Healing calculation coefficients
        private const double WisdomHealingCoefficient = 0.007;
        private const double IntelligenceHealingCoefficient = 0.002;

        // Critical chance calculation
        private const double MaxCriticalChance = 0.5;
        private const double DexterityCritCoefficient = 0.0008;
        private const double LuckCritCoefficient = 0.0006;

        // Attribute caps
        private const int DefaultSoftCap = 250;
        private const int DefaultHardCap = 400;

        /// <summary>
        /// Calculates maximum health based on character level and vitality.
        /// </summary>
        /// <param name="level">Character level</param>
        /// <param name="vitality">Vitality attribute value</param>
        /// <returns>Maximum health value</returns>
        public static int CalculateHealth(int level, int vitality) 
            => BaseHealth + level * HealthPerLevel + vitality * HealthPerVitality;

        /// <summary>
        /// Calculates maximum mana based on character level, intelligence, and wisdom.
        /// </summary>
        /// <param name="level">Character level</param>
        /// <param name="intelligence">Intelligence attribute value</param>
        /// <param name="wisdom">Wisdom attribute value</param>
        /// <returns>Maximum mana value</returns>
        public static int CalculateMana(int level, int intelligence, int wisdom) 
            => BaseMana + level * ManaPerLevel + intelligence * ManaPerIntelligence + wisdom * ManaPerWisdom;

        /// <summary>
        /// Calculates maximum stamina based on character level, dexterity, and vitality.
        /// </summary>
        /// <param name="level">Character level</param>
        /// <param name="dexterity">Dexterity attribute value</param>
        /// <param name="vitality">Vitality attribute value</param>
        /// <returns>Maximum stamina value</returns>
        public static int CalculateStamina(int level, int dexterity, int vitality) 
            => BaseStamina + level * StaminaPerLevel + dexterity * StaminaPerDexterity + vitality * StaminaPerVitality;

        /// <summary>
        /// Calculates physical damage output based on weapon damage, strength, and dexterity.
        /// </summary>
        /// <param name="weaponDamage">Base weapon damage value</param>
        /// <param name="strength">Strength attribute value</param>
        /// <param name="dexterity">Dexterity attribute value</param>
        /// <returns>Final physical damage value</returns>
        public static double CalculatePhysicalDamage(double weaponDamage, int strength, int dexterity)
            => weaponDamage * (1 + strength * StrengthDamageCoefficient + dexterity * DexterityPhysicalDamageCoefficient);

        /// <summary>
        /// Calculates spell damage output based on spell power, intelligence, and wisdom.
        /// </summary>
        /// <param name="spellPower">Base spell power value</param>
        /// <param name="intelligence">Intelligence attribute value</param>
        /// <param name="wisdom">Wisdom attribute value</param>
        /// <returns>Final spell damage value</returns>
        public static double CalculateSpellDamage(double spellPower, int intelligence, int wisdom)
            => spellPower * (1 + intelligence * IntelligenceDamageCoefficient + wisdom * WisdomDamageCoefficient);

        /// <summary>
        /// Calculates healing effectiveness based on base heal amount, wisdom, and intelligence.
        /// </summary>
        /// <param name="baseHeal">Base healing amount</param>
        /// <param name="wisdom">Wisdom attribute value</param>
        /// <param name="intelligence">Intelligence attribute value</param>
        /// <returns>Final healing value</returns>
        public static double CalculateHealing(double baseHeal, int wisdom, int intelligence)
            => baseHeal * (1 + wisdom * WisdomHealingCoefficient + intelligence * IntelligenceHealingCoefficient);

        /// <summary>
        /// Calculates critical hit chance based on base chance, dexterity, and luck.
        /// Critical chance is capped at 50%.
        /// </summary>
        /// <param name="baseCrit">Base critical chance (0.0 to 1.0)</param>
        /// <param name="dexterity">Dexterity attribute value</param>
        /// <param name="luck">Luck attribute value</param>
        /// <returns>Final critical chance value (0.0 to 0.5)</returns>
        public static double CalculateCriticalChance(double baseCrit, int dexterity, int luck)
            => Math.Min(MaxCriticalChance, baseCrit + dexterity * DexterityCritCoefficient + luck * LuckCritCoefficient);

        /// <summary>
        /// Applies soft and hard caps to attribute values to prevent extreme scaling.
        /// Values between soft cap and hard cap have diminished returns (50% effectiveness).
        /// Values at or above hard cap are clamped to hard cap.
        /// </summary>
        /// <param name="value">Attribute value to cap</param>
        /// <param name="softCap">Soft cap threshold (default 250)</param>
        /// <param name="hardCap">Hard cap threshold (default 400)</param>
        /// <returns>Capped attribute value</returns>
        public static int ApplyAttributeCaps(int value, int softCap = DefaultSoftCap, int hardCap = DefaultHardCap)
        {
            if (value <= softCap) return value;
            if (value <= hardCap) return softCap + (value - softCap) / 2;
            return hardCap;
        }

        /// <summary>
        /// Clamps a stat value using soft and hard caps (double version).
        /// </summary>
        /// <param name="value">Stat value to clamp</param>
        /// <param name="softCap">Soft cap threshold (default 250)</param>
        /// <param name="hardCap">Hard cap threshold (default 400)</param>
        /// <returns>Clamped stat value</returns>
        public static double ClampStat(double value, int softCap = DefaultSoftCap, int hardCap = DefaultHardCap)
        {
            if (value <= softCap) return value;
            if (value <= hardCap) return Math.Min(value, hardCap);
            return hardCap;
        }

        /// <summary>
        /// Calculates all character stats in a single operation.
        /// Uses documented formulas to compute health, mana, stamina, damage, and critical chance.
        /// </summary>
        /// <param name="level">Character level</param>
        /// <param name="attributes">Character attributes (strength, dexterity, intelligence, vitality, wisdom, luck)</param>
        /// <param name="weaponDamage">Base weapon damage</param>
        /// <param name="spellPower">Base spell power</param>
        /// <param name="baseCrit">Base critical chance</param>
        /// <returns>Complete stat block with all calculated values</returns>
        public static CharacterStatBlock Calculate(int level, CharacterAttributes attributes, decimal weaponDamage, decimal spellPower, decimal baseCrit)
        {
            var physicalDamage = CalculatePhysicalDamage((double)weaponDamage, attributes.Strength, attributes.Dexterity);
            var spellDamage = CalculateSpellDamage((double)spellPower, attributes.Intelligence, attributes.Wisdom);
            var criticalChance = CalculateCriticalChance((double)baseCrit, attributes.Dexterity, attributes.Luck);

            return new CharacterStatBlock(
                MaxHealth: (decimal)CalculateHealth(level, attributes.Vitality),
                MaxMana: (decimal)CalculateMana(level, attributes.Intelligence, attributes.Wisdom),
                MaxStamina: (decimal)CalculateStamina(level, attributes.Dexterity, attributes.Vitality),
                PhysicalDamage: (decimal)physicalDamage,
                SpellDamage: (decimal)spellDamage,
                CriticalChance: (decimal)criticalChance
            );
        }
    }
}
