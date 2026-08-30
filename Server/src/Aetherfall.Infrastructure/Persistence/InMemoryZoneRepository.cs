using Aetherfall.Application.World;
using Aetherfall.Domain.Common;
using Aetherfall.Domain.World;

namespace Aetherfall.Infrastructure.Persistence;

public sealed class InMemoryZoneRepository : IZoneRepository
{
    private readonly IReadOnlyCollection<ZoneAggregate> _zones;

    public InMemoryZoneRepository()
    {
        var zone = new ZoneAggregate(Guid.NewGuid(), "zone.bastion-foothills", "Bastion Foothills", ZoneType.Overworld);
        zone.AddNpc(new NpcDefinition("npc.elara", "Captain Elara", false, true));
        zone.AddNpc(new NpcDefinition("npc.tobin", "Quartermaster Tobin", true, false));
        zone.AddEncounter(new DungeonEncounterDefinition("encounter.ember-vault", "Ember Vault", 5, 6));
        _zones = new[] { zone };
    }

    public Task<IReadOnlyCollection<ZoneAggregate>> GetAllAsync(CancellationToken cancellationToken) => Task.FromResult(_zones);
}
