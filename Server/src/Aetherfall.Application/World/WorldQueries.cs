using Aetherfall.Contracts.World;
using Aetherfall.Domain.World;

namespace Aetherfall.Application.World;

public interface IZoneRepository
{
    Task<IReadOnlyCollection<ZoneAggregate>> GetAllAsync(CancellationToken cancellationToken);
}

public sealed class WorldQueryService
{
    private readonly IZoneRepository _zones;

    public WorldQueryService(IZoneRepository zones)
    {
        _zones = zones;
    }

    public async Task<IReadOnlyCollection<ZoneResponse>> GetZonesAsync(CancellationToken cancellationToken)
    {
        var zones = await _zones.GetAllAsync(cancellationToken);
        return zones.Select(x => new ZoneResponse(x.ZoneId, x.DisplayName, x.ZoneType.ToString(), x.Npcs.Select(n => n.Name).ToArray(), x.Encounters.Select(e => e.Name).ToArray())).ToArray();
    }
}
