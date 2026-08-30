using Aetherfall.Application.Abstractions;
using Aetherfall.Contracts.Characters;
using Aetherfall.Domain.Characters;
using Aetherfall.Domain.Inventory;

namespace Aetherfall.Application.Characters;

public sealed record CreateCharacterCommand(string AccountId, string Name, Domain.Common.CharacterClassType ClassType, int Strength, int Dexterity, int Intelligence, int Vitality, int Wisdom, int Luck);

public sealed class CreateCharacterHandler : ICommandHandler<CreateCharacterCommand, CharacterSummaryResponse>
{
    private readonly ICharacterRepository _characters;

    public CreateCharacterHandler(ICharacterRepository characters)
    {
        _characters = characters;
    }

    public async Task<Result<CharacterSummaryResponse>> HandleAsync(CreateCharacterCommand command, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(command.AccountId, out _)) return Result<CharacterSummaryResponse>.Failure("Account id is invalid.");
        if (string.IsNullOrWhiteSpace(command.Name) || command.Name.Length < 3) return Result<CharacterSummaryResponse>.Failure("Character name must be at least 3 characters.");

        var attributes = new CharacterAttributes(command.Strength, command.Dexterity, command.Intelligence, command.Vitality, command.Wisdom, command.Luck);
        var stats = CharacterFormulaService.Calculate(1, attributes, 12m, 14m, 0.05m);
        var character = new CharacterAggregate(Guid.NewGuid(), command.AccountId, command.Name, command.ClassType, attributes, stats, new InventoryAggregate(Guid.NewGuid(), 120));
        await _characters.AddAsync(character, cancellationToken);

        return Result<CharacterSummaryResponse>.Success(new CharacterSummaryResponse(character.Id, character.Name, character.ClassType, character.Progression.Level, character.Stats.MaxHealth, character.Stats.MaxMana, character.Stats.MaxStamina));
    }
}
