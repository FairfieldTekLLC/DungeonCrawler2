using System;
using Aetherfall.Domain.Common;

namespace Aetherfall.Domain.Combat
{
    public static class CombatFormulaService
    {
        // Attack damage multipliers
        private const decimal LightAttackMultiplier = 1.0m;
        private const decimal HeavyAttackMultiplier = 1.65m;

        // Stamina costs per action
        private const decimal LightAttackStaminaCost = 8m;
        private const decimal HeavyAttackStaminaCost = 18m;
        private const decimal BlockStaminaCost = 12m;
        private const decimal DodgeStaminaCost = 20m;

        // Mitigation and defense
        private const decimal HeavyAttackBlockReduction = -0.15m;
        private const decimal ArmorMitigationDivisor = 1000m;
        private const decimal MaxMitigationCap = 0.85m;

        // Critical hit mechanics
        private const decimal CriticalHitThreshold = 0.92m;
        private const decimal CriticalDamageMultiplier = 1.5m;

        /// <summary>
        /// Resolves a combat action between an attacker and defender.
        /// Returns a CombatResolution record containing damage dealt, stamina spent, and outcome flags.
        /// </summary>
        public static CombatResolution Resolve(CombatActionType actionType, CombatSnapshot attacker, CombatSnapshot defender, decimal critChanceSeed)
        {
            if (critChanceSeed is < 0 or > 1) throw new ArgumentOutOfRangeException(nameof(critChanceSeed));

            var baseDamage = actionType switch
            {
                CombatActionType.LightAttack => attacker.WeaponDamage * LightAttackMultiplier,
                CombatActionType.HeavyAttack => attacker.WeaponDamage * HeavyAttackMultiplier,
                CombatActionType.Block => 0m,
                CombatActionType.Dodge => 0m,
                _ => throw new ArgumentOutOfRangeException(nameof(actionType))
            };

            var staminaSpent = actionType switch
            {
                CombatActionType.LightAttack => LightAttackStaminaCost,
                CombatActionType.HeavyAttack => HeavyAttackStaminaCost,
                CombatActionType.Block => BlockStaminaCost,
                CombatActionType.Dodge => DodgeStaminaCost,
                _ => 0m
            };

            var dodged = defender.IsDodging && critChanceSeed <= defender.DodgeChance;
            if (dodged)
            {
                return new CombatResolution(0m, staminaSpent, true, false, false);
            }

            var mitigation = defender.IsBlocking
                ? defender.BlockMitigation + (actionType == CombatActionType.HeavyAttack ? HeavyAttackBlockReduction : 0m)
                : defender.Armor / ArmorMitigationDivisor;

            mitigation = Math.Clamp(mitigation, 0m, MaxMitigationCap);
            var critical = critChanceSeed >= CriticalHitThreshold;
            var damage = baseDamage * (1 - mitigation) * (critical ? CriticalDamageMultiplier : 1m);

            return new CombatResolution(decimal.Round(Math.Max(0, damage), 2), staminaSpent, false, defender.IsBlocking, critical);
        }

        /// <summary>
        /// Calculates DPS and duration for a persistent effect or attack.
        /// </summary>
        public static (double dps, int duration) CalculateDpsAndDuration(decimal baseDamage, int duration)
        {
            return ((double)baseDamage, duration);
        }
    }
}
