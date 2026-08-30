using Aetherfall.Domain.Combat;
using Aetherfall.Domain.Common;

namespace Aetherfall.Domain.Tests.Combat;

public sealed class CombatFormulaServiceTests
{
    [Fact]
    public void Resolve_HeavyAttackAgainstBlock_ReducesMitigation()
    {
        var attacker = new CombatSnapshot(Guid.NewGuid(), 5, 50m, 0m, 100m, 0.3m, 0.1m, false, false);
        var defender = new CombatSnapshot(Guid.NewGuid(), 5, 30m, 0m, 150m, 0.45m, 0.1m, true, false);

        var result = CombatFormulaService.Resolve(CombatActionType.HeavyAttack, attacker, defender, 0.5m);

        Assert.Equal(57.75m, result.DamageDealt);
        Assert.True(result.WasBlocked);
        Assert.False(result.WasDodged);
    }
}
