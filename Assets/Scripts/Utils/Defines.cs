namespace Defines
{
    public enum ItemType
    {
        None,
        Equipable,
        Consumable,
        Passive
    }

    public enum EquipType
    {
        None,
        Weapon
    }

    public enum ConsumeableType
    {
        None,
        Health,
        Mana,
        Stamina
    }

    public enum PassiveType
    {
        None,
        Damage,
        Speed
    }
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    public enum ResourceType
    {
        Exp,
        Gold
    }
    public enum SODataType
    {
        None,
        Item
        // 추후에 스킬 추가?
    }

    public enum SOItemDataType
    {
        None,
        Consumable,
        Weapon,
        Passive
    }
}