namespace Aetherfall.Domain.Characters;

public sealed class CharacterProgression
{
    public CharacterProgression(int level, int experience)
    {
        if (level <= 0) throw new ArgumentOutOfRangeException(nameof(level));
        if (experience < 0) throw new ArgumentOutOfRangeException(nameof(experience));
        Level = level;
        Experience = experience;
    }

    public int Level { get; private set; }
    public int Experience { get; private set; }

    public void GainExperience(int amount)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Experience += amount;

        while (Experience >= RequiredExperienceForNextLevel(Level))
        {
            Experience -= RequiredExperienceForNextLevel(Level);
            Level++;
        }
    }

    public static int RequiredExperienceForNextLevel(int level) => 100 + (level * 25);
}
