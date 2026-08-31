using System;
using Aetherfall.Application.Abstractions;
using Aetherfall.Contracts.Combat;
using Aetherfall.Domain.Combat;

namespace Aetherfall.Application.Combat
{
    public sealed record ResolveCombatCommand(string Action, Guid CharacterId);

    public sealed class ResolveCombatHandler : ICommandHandler<ResolveCombatCommand, CombatResolutionResponse>
    {
        public async Task<Result<CombatResolutionResponse>> HandleAsync(ResolveCombatCommand command, CancellationToken cancellationToken)
        {
            var attacker = new CombatSnapshot(); // Simplified for build; replace with repository lookup in production
            var defender = new CombatSnapshot(); // Simplified for build
            
            var combatActionType = (CombatActionType)Enum.Parse(typeof(CombatActionType), command.Action);
            var resolution = CombatFormulaService.Resolve(combatActionType, attacker, defender, 0.5m);
            
            return Result<CombatResolutionResponse>.Success(new CombatResolutionResponse(resolution.Damage, resolution.StaminaSpent, resolution.IsDodged, resolution.IsBlocking, resolution.IsCritical));
        }
    }
}
