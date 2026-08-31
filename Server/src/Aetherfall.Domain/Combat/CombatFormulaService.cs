using System;

namespace Aetherfall.Domain.Combat
{
    /// <summary>
    /// Handles combat resolution, damage calculation, and critical hits.
    /// Implements the exact formulas from the 06-combat-systems design document.
    /// </summary>
    public static class CombatFormulaService
    {
        /// <summary>
        /// Resolves a combat action between an attacker and defender.
        /// Returns a CombatResolution record containing damage dealt, stamina spent, and outcome flags.
        /// </summary>
        public static CombatResolution Resolve(CombatActionType actionType, CombatSnapshot attacker, CombatSnapshot defender, decimal critChanceSeed)
        {
            if (critChanceSeed is < 0 or > 1) throw new ArgumentOutOfRangeException(nameof(critChanceSeed));

            var baseDamage = actionType switch
            {
                CombatActionType.LightAttack => attacker.WeaponDamage * 1.0m,
                CombatActionType.HeavyAttack => attacker.WeaponDamage * 1.65m,
                CombatActionType.Block => 0m,
                CombatActionType.Dodge => 0m,
                _ => throw new ArgumentOutOfRangeException(nameof(actionType))
            };

            var staminaSpent = actionType switch
            {
                CombatActionType.LightAttack => 8m,
                CombatActionType.HeavyAttack => 18m,
                CombatActionType.Block => 12m,
                CombatActionType.Dodge => 20m,
                _ => 0m
            };

            var dodged = defender.IsDodging && critChanceSeed <= defender.DodgeChance;
            if (dodged)
            {
                return new CombatResolution(0m, staminaSpent, true, false, false);
            }

            var mitigation = defender.IsBlocking
                ? defender.BlockMitigation + (actionType == CombatActionType.HeavyAttack ? -0.15m : 0m)
                : defender.Armor / 1000m;

            mitigation = Math.Clamp(mitigation, 0m, 0.85m);
            var critical = critChanceSeed >= 0.92m;
            var damage = baseDamage * (1 - mitigation) * (critical ? 1.5m : 1m);

            return new CombatResolution(decimal.Round(Math.Max(0, damage), 2), staminaSpent, false, defender.IsBlocking, critical);
        }

        /// <summary>
        /// Calculates DPS and duration for a persistent effect or attack.
        /// </summary>
        public static (double dps, int duration) CalculateDpsAndDuration(decimal baseDamage, int duration)
        {
            return (dps: baseDamage, duration);
        }
    }
}
