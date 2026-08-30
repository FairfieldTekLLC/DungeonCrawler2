using UnityEngine;

namespace Aetherfall.Client.Data;

public enum ClientClassRole
{
    Tank,
    Bruiser,
    RangedDps,
    Control
}

[CreateAssetMenu(menuName = "Aetherfall/Character/Class")]
public sealed class ClassDefinition : AetherfallDefinition
{
    [SerializeField] private ClientClassRole primaryRole;
    [SerializeField] private AbilityDefinition[] startingAbilities = new AbilityDefinition[0];

    public ClientClassRole PrimaryRole => primaryRole;
    public AbilityDefinition[] StartingAbilities => startingAbilities;
}
