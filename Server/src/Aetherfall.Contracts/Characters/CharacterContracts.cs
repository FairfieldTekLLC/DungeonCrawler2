using Aetherfall.Domain.Common;

namespace Aetherfall.Contracts.Characters;

public sealed record CreateCharacterRequest(string AccountId, string Name, CharacterClassType ClassType, int Strength, int Dexterity, int Intelligence, int Vitality, int Wisdom, int Luck);
public sealed record CharacterSummaryResponse(Guid CharacterId, string Name, CharacterClassType ClassType, int Level, decimal MaxHealth, decimal MaxMana, decimal MaxStamina);
