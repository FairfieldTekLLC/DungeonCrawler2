using System;
using Aetherfall.Application.Abstractions;
using Aetherfall.Contracts.Combat;
using Aetherfall.Domain.Combat;
using Aetherfall.Domain.Common;

namespace Aetherfall.Application.Combat
{
    public sealed record ResolveCombatCommand(string Action, Guid CharacterId);

    public sealed class ResolveCombatHandler : ICommandHandler<ResolveCombatCommand, CombatResolutionResponse>
    {
        public async Task<Result<CombatResolutionResponse>> HandleAsync(ResolveCombatCommand command, CancellationToken cancellationToken)
        {
            var dummyGuid = Guid.NewGuid();
            var attacker = new CombatSnapshot(dummyGuid, 0, 100m, 100m, 100m, 100m, 100m, false, false);
            var defender = new CombatSnapshot(dummyGuid, 0, 100m, 100m, 100m, 100m, 100m, false, false);

            var combatActionType = Enum.Parse<CombatActionType>(command.Action);
            var resolution = CombatFormulaService.Resolve(combatActionType, attacker, defender, 0.5m);

            // Map properties correctly to the record definition (DamageDealt, WasDodged, etc.)
            return Result<CombatResolutionResponse>.Success(new CombatResolutionResponse(
                resolution.DamageDealt, 
                resolution.StaminaSpent, 
                resolution.WasDodged, 
                resolution.WasBlocked, 
                resolution.WasCritical));
        }
    }
}
