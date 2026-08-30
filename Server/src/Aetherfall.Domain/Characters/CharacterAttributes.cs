namespace Aetherfall.Domain.Characters;

public sealed record CharacterAttributes
{
    public CharacterAttributes(int strength, int dexterity, int intelligence, int vitality, int wisdom, int luck)
    {
        if (strength < 0 || dexterity < 0 || intelligence < 0 || vitality < 0 || wisdom < 0 || luck < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(strength), "Attributes must be non-negative.");
        }

        Strength = strength;
        Dexterity = dexterity;
        Intelligence = intelligence;
        Vitality = vitality;
        Wisdom = wisdom;
        Luck = luck;
    }

    public int Strength { get; }
    public int Dexterity { get; }
    public int Intelligence { get; }
    public int Vitality { get; }
    public int Wisdom { get; }
    public int Luck { get; }
}
