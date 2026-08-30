# ScriptableObject Definitions

```csharp
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
    public Role PrimaryRole;
    public AbilityDefinition[] StartingAbilities;
    public SpecializationDefinition[] Specializations;
    public TalentTreeDefinition TalentTree;
}

[CreateAssetMenu(menuName = "Aetherfall/Combat/Ability")]
public sealed class AbilityDefinition : AetherfallDefinition
{
    public float CooldownSeconds;
    public float ResourceCost;
    public TargetingMode TargetingMode;
    public EffectDefinition[] Effects;
    public StatusEffectDefinition[] AppliedStatusEffects;
}

[CreateAssetMenu(menuName = "Aetherfall/Items/Item")]
public sealed class ItemDefinition : AetherfallDefinition
{
    public ItemCategory Category;
    public EquipmentSlot Slot;
    public Rarity MinimumRarity;
    public StatModifier[] BaseStats;
    public SocketRule SocketRule;
}

[CreateAssetMenu(menuName = "Aetherfall/Companion/Companion")]
public sealed class CompanionDefinition : AetherfallDefinition
{
    public CompanionClass Class;
    public PersonalityTraits BaseTraits;
    public TalentTreeDefinition ClassTree;
    public QuestDefinition[] PersonalQuests;
}
```

ScriptableObjects are immutable runtime definitions. Player state stores only IDs, rolled values, and progression; services resolve definitions through injected repositories.
