namespace Aetherfall.Domain.Characters;

public sealed record CharacterStatBlock(decimal MaxHealth, decimal MaxMana, decimal MaxStamina, decimal PhysicalDamage, decimal SpellDamage, decimal CriticalChance);
