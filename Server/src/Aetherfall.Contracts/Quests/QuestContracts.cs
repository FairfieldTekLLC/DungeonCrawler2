namespace Aetherfall.Contracts.Quests;

public sealed record AcceptQuestRequest(Guid CharacterId, string QuestDefinitionId);
public sealed record AdvanceQuestObjectiveRequest(Guid CharacterId, string QuestDefinitionId, string ObjectiveId, int Amount);
