using Aetherfall.Application.Abstractions;

namespace Aetherfall.Application.Companions;

public sealed record RecruitCompanionCommand(Guid CharacterId, string CompanionDefinitionId);

public sealed class RecruitCompanionHandler : ICommandHandler<RecruitCompanionCommand, bool>
{
    private readonly ICharacterRepository _characters;
    private readonly ICompanionDefinitionRepository _companions;

    public RecruitCompanionHandler(ICharacterRepository characters, ICompanionDefinitionRepository companions)
    {
        _characters = characters;
        _companions = companions;
    }

    public async Task<Result<bool>> HandleAsync(RecruitCompanionCommand command, CancellationToken cancellationToken)
    {
        var character = await _characters.GetByIdAsync(command.CharacterId, cancellationToken);
        if (character is null) return Result<bool>.Failure("Character not found.");
        var companion = await _companions.CreateAsync(command.CompanionDefinitionId, cancellationToken);
        if (companion is null) return Result<bool>.Failure("Companion definition not found.");
        character.RecruitCompanion(companion);
        await _characters.UpdateAsync(character, cancellationToken);
        return Result<bool>.Success(true);
    }
}
