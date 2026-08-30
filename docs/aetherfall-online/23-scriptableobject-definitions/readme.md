# ScriptableObject Definitions

```csharp
using System.Collections.Generic;
using UnityEngine;

public abstract class AetherfallDefinition : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private string displayName;
    [SerializeField, TextArea] private string description;

    public string Id => id;
    public string DisplayName => displayName;
    public string Description => description;
}

[CreateAssetMenu(menuName = "Aetherfall/Character/Class")]
public sealed class ClassDefinition : AetherfallDefinition
{
    [SerializeField] private Role primaryRole;
    [SerializeField] private AbilityDefinition[] startingAbilities;
    [SerializeField] private SpecializationDefinition[] specializations;
    [SerializeField] private TalentTreeDefinition talentTree;

    public Role PrimaryRole => primaryRole;
    public IReadOnlyList<AbilityDefinition> StartingAbilities => startingAbilities;
    public IReadOnlyList<SpecializationDefinition> Specializations => specializations;
    public TalentTreeDefinition TalentTree => talentTree;
}

[CreateAssetMenu(menuName = "Aetherfall/Combat/Ability")]
public sealed class AbilityDefinition : AetherfallDefinition
{
    [SerializeField] private float cooldownSeconds;
    [SerializeField] private float resourceCost;
    [SerializeField] private TargetingMode targetingMode;
    [SerializeField] private EffectDefinition[] effects;
    [SerializeField] private StatusEffectDefinition[] appliedStatusEffects;

    public float CooldownSeconds => cooldownSeconds;
    public float ResourceCost => resourceCost;
    public TargetingMode TargetingMode => targetingMode;
    public IReadOnlyList<EffectDefinition> Effects => effects;
    public IReadOnlyList<StatusEffectDefinition> AppliedStatusEffects => appliedStatusEffects;
}

[CreateAssetMenu(menuName = "Aetherfall/Items/Item")]
public sealed class ItemDefinition : AetherfallDefinition
{
    [SerializeField] private ItemCategory category;
    [SerializeField] private EquipmentSlot slot;
    [SerializeField] private Rarity minimumRarity;
    [SerializeField] private StatModifier[] baseStats;
    [SerializeField] private SocketRule socketRule;

    public ItemCategory Category => category;
    public EquipmentSlot Slot => slot;
    public Rarity MinimumRarity => minimumRarity;
    public IReadOnlyList<StatModifier> BaseStats => baseStats;
    public SocketRule SocketRule => socketRule;
}

[CreateAssetMenu(menuName = "Aetherfall/Companion/Companion")]
public sealed class CompanionDefinition : AetherfallDefinition
{
    [SerializeField] private CompanionClass companionClass;
    [SerializeField] private PersonalityTraits baseTraits;
    [SerializeField] private TalentTreeDefinition classTree;
    [SerializeField] private QuestDefinition[] personalQuests;

    public CompanionClass Class => companionClass;
    public PersonalityTraits BaseTraits => baseTraits;
    public TalentTreeDefinition ClassTree => classTree;
    public IReadOnlyList<QuestDefinition> PersonalQuests => personalQuests;
}
```

ScriptableObjects are immutable runtime definitions. Player state stores only IDs, rolled values, and progression; services resolve definitions through injected repositories.
