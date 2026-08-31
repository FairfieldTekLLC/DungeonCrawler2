using System;

namespace Aetherfall.Domain.Characters
{
    public static class CharacterFormulaService
    {
        public static int CalculateHealth(int level, int vitality) => 100 + level * 18 + vitality * 12;
        public static int CalculateMana(int level, int intelligence, int wisdom) => 50 + level * 8 + intelligence * 8 + wisdom * 4;
        public static int CalculateStamina(int level, int dexterity, int vitality) => 75 + level * 6 + dexterity * 3 + vitality * 5;

        public static double CalculatePhysicalDamage(double weaponDamage, int strength, int dexterity)
            => weaponDamage * (1 + strength * 0.006 + dexterity * 0.002);

        public static double CalculateSpellDamage(double spellPower, int intelligence, int wisdom)
            => spellPower * (1 + intelligence * 0.007 + wisdom * 0.002);

        public static double CalculateHealing(double baseHeal, int wisdom, int intelligence)
            => baseHeal * (1 + wisdom * 0.007 + intelligence * 0.002);

        public static double CalculateCriticalChance(double baseCrit, int dexterity, int luck)
            => Math.Min(0.5, baseCrit + dexterity * 0.0008 + luck * 0.0006);

        public static int ApplyAttributeCaps(int value, int softCap = 250, int hardCap = 400)
        {
            if (value <= softCap) return value;
            if (value <= hardCap) return softCap + (value - softCap) / 2;
            return hardCap;
        }

        public static double ClampStat(double value, int softCap = 250, int hardCap = 400)
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
                CriticalChance: criticalChance
            );
        }
    }
}
