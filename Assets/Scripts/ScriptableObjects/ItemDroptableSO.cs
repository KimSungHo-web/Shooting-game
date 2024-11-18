using Defines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropTable", menuName = "Datas/DropTable")]
public class ItemDroptableSO : ScriptableObject
{
    [System.Serializable]
    public class DropItem
    {
        public ItemSO item;           
        public float dropChance;     
    }

    [Header("아이템 드롭 리스트")]
    public List<DropItem> commonItems;
    public List<DropItem> uncommonItems;
    public List<DropItem> rareItems;
    public List<DropItem> epicItems;
    public List<DropItem> legendaryItems;

    public List<DropItem> GetItemsByRarity(Rarity rarity)
    {
        return rarity switch
        {
            Rarity.Common => commonItems,
            Rarity.Uncommon => uncommonItems,
            Rarity.Rare => rareItems,
            Rarity.Epic => epicItems,
            Rarity.Legendary => legendaryItems,
            _ => new List<DropItem>()
        };
    }
}
