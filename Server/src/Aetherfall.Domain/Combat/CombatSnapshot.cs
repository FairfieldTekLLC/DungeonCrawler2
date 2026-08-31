using Aetherfall.Domain.Common;

namespace Aetherfall.Domain.Combat;

public sealed record CombatSnapshot(Guid EntityId, int Level, decimal WeaponDamage, decimal SpellPower, decimal Armor, decimal BlockMitigation, decimal DodgeChance, bool IsBlocking, bool IsDodging);

public sealed record CombatResolution(decimal DamageDealt, decimal StaminaSpent, bool WasDodged, bool WasBlocked, bool WasCritical);
