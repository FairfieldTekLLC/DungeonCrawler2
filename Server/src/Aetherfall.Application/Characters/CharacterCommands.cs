using Aetherfall.Application.Abstractions;
using Aetherfall.Application.Common;
using Aetherfall.Contracts.Characters;
using Aetherfall.Domain.Characters;
using Aetherfall.Domain.Inventory;

namespace Aetherfall.Application.Characters;

public sealed record CreateCharacterCommand(string AccountId, string Name, Domain.Common.CharacterClassType ClassType, int Strength, int Dexterity, int Intelligence, int Vitality, int Wisdom, int Luck);

public sealed class CreateCharacterHandler : ICommandHandler<CreateCharacterCommand, CharacterSummaryResponse>
{
    // Initial character stats
    private const int StartingLevel = 1;
    private const decimal StartingWeaponDamage = 12m;
    private const decimal StartingSpellPower = 14m;
    private const decimal StartingCriticalChance = 0.05m;
    private const int StartingInventoryCapacity = 120;

    private readonly ICharacterRepository _characters;

    public CreateCharacterHandler(ICharacterRepository characters)
    {
        _characters = characters;
    }

    public async Task<Result<CharacterSummaryResponse>> HandleAsync(CreateCharacterCommand command, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(command.AccountId, out _)) return Result<CharacterSummaryResponse>.Failure("Account id is invalid.");
        if (string.IsNullOrWhiteSpace(command.Name) || command.Name.Length < ValidationConstants.MinCharacterNameLength) 
            return Result<CharacterSummaryResponse>.Failure($"Character name must be at least {ValidationConstants.MinCharacterNameLength} characters.");

        var attributes = new CharacterAttributes(command.Strength, command.Dexterity, command.Intelligence, command.Vitality, command.Wisdom, command.Luck);
        var stats = CharacterFormulaService.Calculate(StartingLevel, attributes, StartingWeaponDamage, StartingSpellPower, StartingCriticalChance);
        var character = new CharacterAggregate(Guid.NewGuid(), command.AccountId, command.Name, command.ClassType, attributes, stats, new InventoryAggregate(Guid.NewGuid(), StartingInventoryCapacity));
        await _characters.AddAsync(character, cancellationToken);

        return Result<CharacterSummaryResponse>.Success(new CharacterSummaryResponse(character.Id, character.Name, character.ClassType, character.Progression.Level, character.Stats.MaxHealth, character.Stats.MaxMana, character.Stats.MaxStamina));
    }
}
