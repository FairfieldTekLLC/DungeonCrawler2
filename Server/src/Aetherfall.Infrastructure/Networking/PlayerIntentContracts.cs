using Aetherfall.Domain.Common;

namespace Aetherfall.Infrastructure.Networking;

public sealed record PlayerIntentEnvelope(Guid CharacterId, CombatActionType ActionType, string TargetEntityId, long ClientTick, string IdempotencyKey);
public sealed record ReplicationEnvelope(string ZoneId, long ServerTick, string PayloadType, string PayloadJson);
