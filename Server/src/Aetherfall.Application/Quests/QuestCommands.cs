using Aetherfall.Application.Abstractions;

namespace Aetherfall.Application.Quests;

public sealed record AcceptQuestCommand(Guid CharacterId, string QuestDefinitionId);
public sealed record AdvanceQuestObjectiveCommand(Guid CharacterId, string QuestDefinitionId, string ObjectiveId, int Amount);

public sealed class AcceptQuestHandler : ICommandHandler<AcceptQuestCommand, bool>
{
    private readonly ICharacterRepository _characters;
    private readonly IQuestDefinitionRepository _quests;

    public AcceptQuestHandler(ICharacterRepository characters, IQuestDefinitionRepository quests)
    {
        _characters = characters;
        _quests = quests;
    }

    public async Task<Result<bool>> HandleAsync(AcceptQuestCommand command, CancellationToken cancellationToken)
    {
        var character = await _characters.GetByIdAsync(command.CharacterId, cancellationToken);
        if (character is null) return Result<bool>.Failure("Character not found.");
        var quest = await _quests.CreateQuestAsync(command.QuestDefinitionId, cancellationToken);
        if (quest is null) return Result<bool>.Failure("Quest definition not found.");
        character.AcceptQuest(quest);
        await _characters.UpdateAsync(character, cancellationToken);
        return Result<bool>.Success(true);
    }
}

public sealed class AdvanceQuestObjectiveHandler : ICommandHandler<AdvanceQuestObjectiveCommand, bool>
{
    private readonly ICharacterRepository _characters;

    public AdvanceQuestObjectiveHandler(ICharacterRepository characters)
    {
        _characters = characters;
    }

    public async Task<Result<bool>> HandleAsync(AdvanceQuestObjectiveCommand command, CancellationToken cancellationToken)
    {
        var character = await _characters.GetByIdAsync(command.CharacterId, cancellationToken);
        if (character is null) return Result<bool>.Failure("Character not found.");
        var quest = character.Quests.FirstOrDefault(x => x.DefinitionId == command.QuestDefinitionId);
        if (quest is null) return Result<bool>.Failure("Quest not accepted.");
        quest.AdvanceObjective(command.ObjectiveId, command.Amount);
        await _characters.UpdateAsync(character, cancellationToken);
        return Result<bool>.Success(true);
    }
}
