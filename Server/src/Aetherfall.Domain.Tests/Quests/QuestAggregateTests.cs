using Aetherfall.Domain.Common;
using Aetherfall.Domain.Quests;

namespace Aetherfall.Domain.Tests.Quests;

public sealed class QuestAggregateTests
{
    [Fact]
    public void AdvanceObjective_CompletesQuestWhenAllObjectivesMet()
    {
        var quest = new QuestAggregate(Guid.NewGuid(), "quest.bastion.wolf-hunt", new[]
        {
            new QuestObjective("wolves", Domain.Quests.ObjectiveType.Kill, 2),
            new QuestObjective("charms", Domain.Quests.ObjectiveType.Collect, 1)
        });

        quest.Start();
        quest.AdvanceObjective("wolves", 2);
        quest.AdvanceObjective("charms", 1);

        Assert.Equal(QuestStatus.Completed, quest.Status);
    }
}
