using Aetherfall.Domain.Common;

namespace Aetherfall.Contracts.Combat;

public sealed record ResolveCombatRequest(CombatActionType ActionType, Guid AttackerId, Guid DefenderId, decimal CritSeed);
public sealed record CombatResolutionResponse(decimal Damage, decimal StaminaSpent, bool WasDodged, bool WasBlocked, bool WasCritical);
