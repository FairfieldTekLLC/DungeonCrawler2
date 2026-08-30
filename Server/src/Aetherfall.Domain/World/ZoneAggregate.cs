using Aetherfall.Domain.Abstractions;
using Aetherfall.Domain.Common;

namespace Aetherfall.Domain.World;

public sealed record NpcDefinition(string Id, string Name, bool IsVendor, bool IsQuestGiver);
public sealed record DungeonEncounterDefinition(string Id, string Name, int RecommendedLevel, int EnemyCount);

public sealed class ZoneAggregate : Entity
{
    private readonly List<NpcDefinition> _npcs = new();
    private readonly List<DungeonEncounterDefinition> _encounters = new();

    public ZoneAggregate(Guid id, string zoneId, string displayName, ZoneType zoneType) : base(id)
    {
        ZoneId = zoneId;
        DisplayName = displayName;
        ZoneType = zoneType;
    }

    public string ZoneId { get; }
    public string DisplayName { get; }
    public ZoneType ZoneType { get; }
    public IReadOnlyCollection<NpcDefinition> Npcs => _npcs.AsReadOnly();
    public IReadOnlyCollection<DungeonEncounterDefinition> Encounters => _encounters.AsReadOnly();

    public void AddNpc(NpcDefinition npc) => _npcs.Add(npc);
    public void AddEncounter(DungeonEncounterDefinition encounter) => _encounters.Add(encounter);
}
