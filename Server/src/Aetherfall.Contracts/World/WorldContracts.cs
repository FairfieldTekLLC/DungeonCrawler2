namespace Aetherfall.Contracts.World;

public sealed record ZoneResponse(string ZoneId, string DisplayName, string ZoneType, IReadOnlyCollection<string> Npcs, IReadOnlyCollection<string> Encounters);
