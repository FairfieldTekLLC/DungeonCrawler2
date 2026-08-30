using Aetherfall.Domain.Abstractions;
using Aetherfall.Domain.Common;
using Aetherfall.Domain.Companions;
using Aetherfall.Domain.Inventory;
using Aetherfall.Domain.Quests;

namespace Aetherfall.Domain.Characters;

public sealed record CharacterCreatedEvent(Guid CharacterId, string Name, CharacterClassType ClassType, DateTimeOffset OccurredAt) : IDomainEvent;
public sealed record CharacterLeveledEvent(Guid CharacterId, int Level, DateTimeOffset OccurredAt) : IDomainEvent;

public sealed class CharacterAggregate : Entity
{
    private readonly List<QuestAggregate> _quests = new();
    private readonly List<CompanionAggregate> _companions = new();

    public CharacterAggregate(
        Guid id,
        string accountId,
        string name,
        CharacterClassType classType,
        CharacterAttributes attributes,
        CharacterStatBlock stats,
        InventoryAggregate inventory) : base(id)
    {
        AccountId = Guard.AgainstNullOrWhiteSpace(accountId, nameof(accountId));
        Name = Guard.AgainstNullOrWhiteSpace(name, nameof(name));
        ClassType = classType;
        Attributes = attributes;
        Progression = new CharacterProgression(1, 0);
        Stats = stats;
        Resources = new CharacterResources(stats.MaxHealth, stats.MaxMana, stats.MaxStamina);
        Inventory = inventory;
        Raise(new CharacterCreatedEvent(Id, Name, ClassType, DateTimeOffset.UtcNow));
    }

    public string AccountId { get; }
    public string Name { get; }
    public CharacterClassType ClassType { get; }
    public CharacterAttributes Attributes { get; }
    public CharacterProgression Progression { get; }
    public CharacterStatBlock Stats { get; private set; }
    public CharacterResources Resources { get; }
    public InventoryAggregate Inventory { get; }
    public IReadOnlyCollection<QuestAggregate> Quests => _quests.AsReadOnly();
    public IReadOnlyCollection<CompanionAggregate> Companions => _companions.AsReadOnly();

    public void RecalculateStats(decimal weaponDamage, decimal spellPower, decimal baseCrit)
    {
        Stats = CharacterFormulaService.Calculate(Progression.Level, Attributes, weaponDamage, spellPower, baseCrit);
    }

    public void GainExperience(int amount)
    {
        var currentLevel = Progression.Level;
        Progression.GainExperience(amount);
        if (Progression.Level > currentLevel)
        {
            Raise(new CharacterLeveledEvent(Id, Progression.Level, DateTimeOffset.UtcNow));
        }
    }

    public void AcceptQuest(QuestAggregate quest)
    {
        ArgumentNullException.ThrowIfNull(quest);
        if (_quests.Any(x => x.DefinitionId == quest.DefinitionId)) throw new InvalidOperationException("Quest already accepted.");
        quest.Start();
        _quests.Add(quest);
    }

    public void RecruitCompanion(CompanionAggregate companion)
    {
        ArgumentNullException.ThrowIfNull(companion);
        if (_companions.Count >= 4) throw new InvalidOperationException("Maximum companions reached.");
        _companions.Add(companion);
    }
}
