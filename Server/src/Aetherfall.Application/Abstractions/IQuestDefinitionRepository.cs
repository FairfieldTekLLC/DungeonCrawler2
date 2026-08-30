using Aetherfall.Domain.Quests;

namespace Aetherfall.Application.Abstractions;

public interface IQuestDefinitionRepository
{
    Task<QuestAggregate?> CreateQuestAsync(string questDefinitionId, CancellationToken cancellationToken);
}
