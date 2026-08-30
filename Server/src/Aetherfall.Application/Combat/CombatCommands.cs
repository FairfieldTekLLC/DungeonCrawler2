using Aetherfall.Application.Abstractions;
using Aetherfall.Contracts.Combat;
using Aetherfall.Domain.Combat;
using Aetherfall.Domain.Common;

namespace Aetherfall.Application.Combat;

public sealed record ResolveCombatCommand(CombatActionType ActionType, Guid AttackerId, Guid DefenderId, decimal CritSeed);

public sealed class ResolveCombatHandler : ICommandHandler<ResolveCombatCommand, CombatResolutionResponse>
{
    private readonly ICharacterRepository _characters;

    public ResolveCombatHandler(ICharacterRepository characters)
    {
        _characters = characters;
    }

    public async Task<Result<CombatResolutionResponse>> HandleAsync(ResolveCombatCommand command, CancellationToken cancellationToken)
    {
        var attacker = await _characters.GetByIdAsync(command.AttackerId, cancellationToken);
        var defender = await _characters.GetByIdAsync(command.DefenderId, cancellationToken);
        if (attacker is null || defender is null) return Result<CombatResolutionResponse>.Failure("Combatants not found.");

        var attackState = new CombatSnapshot(attacker.Id, attacker.Progression.Level, attacker.Stats.PhysicalDamage, attacker.Stats.SpellDamage, 120m, 0.35m, 0.1m, false, false);
        var defendState = new CombatSnapshot(defender.Id, defender.Progression.Level, defender.Stats.PhysicalDamage, defender.Stats.SpellDamage, 160m, 0.45m, 0.18m, command.ActionType == CombatActionType.Block, command.ActionType == CombatActionType.Dodge);
        var resolution = CombatFormulaService.Resolve(command.ActionType, attackState, defendState, command.CritSeed);

        defender.Resources.ApplyDamage(resolution.DamageDealt);
        attacker.Resources.SpendStamina(resolution.StaminaSpent);
        await _characters.UpdateAsync(attacker, cancellationToken);
        await _characters.UpdateAsync(defender, cancellationToken);

        return Result<CombatResolutionResponse>.Success(new CombatResolutionResponse(resolution.DamageDealt, resolution.StaminaSpent, resolution.WasDodged, resolution.WasBlocked, resolution.WasCritical));
    }
}
