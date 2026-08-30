using Aetherfall.Domain.Abstractions;
using Aetherfall.Domain.Characters;
using Aetherfall.Domain.Common;

namespace Aetherfall.Domain.Companions;

public sealed record PersonalityProfile(int Loyalty, int Courage, int Compassion, int Aggression, int Wisdom)
{
    public int UtilityScore => Loyalty + Courage + Compassion + Aggression + Wisdom;
}

public sealed class CompanionAggregate : Entity
{
    public CompanionAggregate(Guid id, string definitionId, CharacterAttributes attributes, PersonalityProfile profile) : base(id)
    {
        DefinitionId = Guard.AgainstNullOrWhiteSpace(definitionId, nameof(definitionId));
        Attributes = attributes;
        Profile = profile;
        RelationshipRank = RelationshipRank.Companion;
        Level = 1;
    }

    public string DefinitionId { get; }
    public CharacterAttributes Attributes { get; }
    public PersonalityProfile Profile { get; }
    public RelationshipRank RelationshipRank { get; private set; }
    public int Level { get; private set; }

    public void GainBond(int value)
    {
        if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
        if (value >= 75) RelationshipRank = RelationshipRank.SoulboundAlly;
        else if (value >= 50) RelationshipRank = RelationshipRank.BestFriend;
        else if (value >= 25) RelationshipRank = RelationshipRank.TrustedAlly;
    }
}
