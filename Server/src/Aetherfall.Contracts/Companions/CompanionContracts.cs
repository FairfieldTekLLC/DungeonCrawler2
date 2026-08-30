namespace Aetherfall.Contracts.Companions;

public sealed record RecruitCompanionRequest(Guid CharacterId, string CompanionDefinitionId);
