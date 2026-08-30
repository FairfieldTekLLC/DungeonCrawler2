using UnityEngine;

namespace Aetherfall.Client.Data;

public enum TargetingMode
{
    Self,
    Enemy,
    Ground
}

[CreateAssetMenu(menuName = "Aetherfall/Combat/Ability")]
public sealed class AbilityDefinition : AetherfallDefinition
{
    [SerializeField] private float cooldownSeconds;
    [SerializeField] private float resourceCost;
    [SerializeField] private TargetingMode targetingMode;

    public float CooldownSeconds => cooldownSeconds;
    public float ResourceCost => resourceCost;
    public TargetingMode TargetingMode => targetingMode;
}
