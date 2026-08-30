namespace Aetherfall.Domain.Common;

public enum CharacterClassType
{
    Warrior = 1,
    Mage = 2
}

public enum ProfessionType
{
    Blacksmithing = 1,
    Alchemy = 2
}

public enum RelationshipRank
{
    Stranger = 0,
    Companion = 1,
    TrustedAlly = 2,
    BestFriend = 3,
    SoulboundAlly = 4
}

public enum QuestStatus
{
    NotStarted = 0,
    InProgress = 1,
    Completed = 2,
    Failed = 3
}

public enum ObjectiveType
{
    Kill = 1,
    Collect = 2,
    Interact = 3,
    Escort = 4
}

public enum ItemCategory
{
    Weapon = 1,
    Armor = 2,
    Consumable = 3,
    Material = 4,
    Quest = 5
}

public enum EquipmentSlot
{
    None = 0,
    MainHand = 1,
    OffHand = 2,
    Chest = 3,
    Trinket = 4
}

public enum Rarity
{
    Common = 1,
    Uncommon = 2,
    Rare = 3,
    Epic = 4,
    Legendary = 5,
    Mythic = 6
}

public enum CombatActionType
{
    LightAttack = 1,
    HeavyAttack = 2,
    Block = 3,
    Dodge = 4
}

public enum ZoneType
{
    Overworld = 1,
    Dungeon = 2
}
