using System;

namespace Aetherfall.Domain.Characters
{
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

        public static int CalculateHealth(int level, int vitality) 
            => BaseHealth + level * HealthPerLevel + vitality * HealthPerVitality;

        public static int CalculateMana(int level, int intelligence, int wisdom) 
            => BaseMana + level * ManaPerLevel + intelligence * ManaPerIntelligence + wisdom * ManaPerWisdom;

        public static int CalculateStamina(int level, int dexterity, int vitality) 
            => BaseStamina + level * StaminaPerLevel + dexterity * StaminaPerDexterity + vitality * StaminaPerVitality;

        public static double CalculatePhysicalDamage(double weaponDamage, int strength, int dexterity)
            => weaponDamage * (1 + strength * StrengthDamageCoefficient + dexterity * DexterityPhysicalDamageCoefficient);

        public static double CalculateSpellDamage(double spellPower, int intelligence, int wisdom)
            => spellPower * (1 + intelligence * IntelligenceDamageCoefficient + wisdom * WisdomDamageCoefficient);

        public static double CalculateHealing(double baseHeal, int wisdom, int intelligence)
            => baseHeal * (1 + wisdom * WisdomHealingCoefficient + intelligence * IntelligenceHealingCoefficient);

        public static double CalculateCriticalChance(double baseCrit, int dexterity, int luck)
            => Math.Min(MaxCriticalChance, baseCrit + dexterity * DexterityCritCoefficient + luck * LuckCritCoefficient);

        public static int ApplyAttributeCaps(int value, int softCap = DefaultSoftCap, int hardCap = DefaultHardCap)
        {
            if (value <= softCap) return value;
            if (value <= hardCap) return softCap + (value - softCap) / 2;
            return hardCap;
        }

        public static double ClampStat(double value, int softCap = DefaultSoftCap, int hardCap = DefaultHardCap)
        {
            if (value <= softCap) return value;
            if (value <= hardCap) return Math.Min(value, hardCap);
            return hardCap;
        }

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
