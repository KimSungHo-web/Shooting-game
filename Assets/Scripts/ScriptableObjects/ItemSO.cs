using Defines;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "S0_ItemData", menuName = "Datas/S0_ItemData")]
public class ItemSO : InteractableSO
{
    public int itemValue;
    public Sprite itemIcon;
    public ItemType itemType;

    [Header("인벤토리설정")]
    public bool isStackable;
    public int stackSize;
    public int SlotSize;

    [Header("장비")]
    public EquipType equipType;
    public GameObject equipPrefab;
    public Rarity rarity;
    public bool LeftHand;
    public string childPath;

    [Header("무기정보(speed는 -면 속도 빨라짐)")]
    public float projectileDamage;
    public float projectileSpeed;
    public float projectileScale;

    [Header("소비")]
    public List<ConsumableData> consumables;
    public GameObject dropItemPrefab;

    [Header("리소스")]
    public ResourceType resourceType;

    [Header("획득에 필요한 재화(int,리소스)")]
    public ResourceData[] needResources;

}
