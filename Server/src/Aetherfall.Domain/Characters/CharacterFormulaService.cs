namespace Aetherfall.Domain.Characters;

public static class CharacterFormulaService
{
    public static decimal ApplySoftCap(int attribute)
    {
        if (attribute <= 250)
        {
            return attribute;
        }

        var capped = 250 + ((attribute - 250) * 0.5m);
        return Math.Min(400, capped);
    }

    public static CharacterStatBlock Calculate(int level, CharacterAttributes attributes, decimal weaponDamage, decimal spellPower, decimal baseCrit)
    {
        if (level <= 0) throw new ArgumentOutOfRangeException(nameof(level));

        var strength = ApplySoftCap(attributes.Strength);
        var dexterity = ApplySoftCap(attributes.Dexterity);
        var intelligence = ApplySoftCap(attributes.Intelligence);
        var vitality = ApplySoftCap(attributes.Vitality);
        var wisdom = ApplySoftCap(attributes.Wisdom);
        var luck = ApplySoftCap(attributes.Luck);

        var health = 100 + (level * 18) + (vitality * 12);
        var mana = 50 + (level * 8) + (intelligence * 8) + (wisdom * 4);
        var stamina = 75 + (level * 6) + (dexterity * 3) + (vitality * 5);
        var physicalDamage = weaponDamage * (1 + (strength * 0.006m) + (dexterity * 0.002m));
        var spellDamage = spellPower * (1 + (intelligence * 0.007m) + (wisdom * 0.002m));
        var criticalChance = Math.Min(0.5m, baseCrit + (dexterity * 0.0008m) + (luck * 0.0006m));

        return new CharacterStatBlock(decimal.Round(health, 2), decimal.Round(mana, 2), decimal.Round(stamina, 2), decimal.Round(physicalDamage, 2), decimal.Round(spellDamage, 2), decimal.Round(criticalChance, 4));
    }
}
