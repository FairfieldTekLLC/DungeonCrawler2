using UnityEngine;

namespace Aetherfall.Client.Data;

public enum ClientItemCategory
{
    Weapon,
    Armor,
    Consumable,
    Material,
    Quest
}

[CreateAssetMenu(menuName = "Aetherfall/Items/Item")]
public sealed class ItemDefinition : AetherfallDefinition
{
    [SerializeField] private ClientItemCategory category;
    [SerializeField] private string equipmentSlot = string.Empty;
    [SerializeField] private string minimumRarity = "Common";

    public ClientItemCategory Category => category;
    public string EquipmentSlot => equipmentSlot;
    public string MinimumRarity => minimumRarity;
}
