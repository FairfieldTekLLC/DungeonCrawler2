using System;
using Aetherfall.Domain.Common;
using Aetherfall.Domain.Combat;

namespace Aetherfall.Application.Combat
{
    public static class CombatCommands
    {
        public static CombatResolution Execute(string action, CombatSnapshot attacker, CombatSnapshot defender, decimal critChanceSeed)
        {
            var combatActionType = (CombatActionType)Enum.Parse(typeof(CombatActionType), action);
            return CombatFormulaService.Resolve(combatActionType, attacker, defender, critChanceSeed);
        }
    }
}
