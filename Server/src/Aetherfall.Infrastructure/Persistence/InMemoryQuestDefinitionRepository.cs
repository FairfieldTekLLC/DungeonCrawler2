using Aetherfall.Application.Abstractions;
using Aetherfall.Domain.Common;
using Aetherfall.Domain.Quests;

namespace Aetherfall.Infrastructure.Persistence;

public sealed class InMemoryQuestDefinitionRepository : IQuestDefinitionRepository
{
    public Task<QuestAggregate?> CreateQuestAsync(string questDefinitionId, CancellationToken cancellationToken)
    {
        QuestAggregate? quest = questDefinitionId switch
        {
            "quest.bastion.wolf-hunt" => new QuestAggregate(Guid.NewGuid(), questDefinitionId, new[]
            {
                new QuestObjective("slay-wolves", ObjectiveType.Kill, 3),
                new QuestObjective("recover-charms", ObjectiveType.Collect, 2)
            }),
            _ => null
        };

        return Task.FromResult(quest);
    }
}
