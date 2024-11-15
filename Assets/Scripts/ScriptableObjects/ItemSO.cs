using Defines;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "S0_ItemData", menuName = "Datas/S0_ItemData")]
public class ItemSO : InteractableSO
{
    public int itemValue;
    public Sprite itemIcon;
    public ItemType itemType;

    [Header("�κ��丮����")]
    public bool isStackable;
    public int stackSize;
    public int SlotSize;

    [Header("���")]
    public EquipType equipType;
    public GameObject equipPrefab;
    public Rarity rarity;
    public bool LeftHand;
    public string childPath;

    [Header("��������(speed�� -�� �ӵ� ������)")]
    public float projectileDamage;
    public float projectileSpeed;
    public float projectileScale;

    [Header("�Һ�")]
    public List<ConsumableData> consumables;
    public GameObject dropItemPrefab;

    [Header("���ҽ�")]
    public ResourceType resourceType;

    [Header("ȹ�濡 �ʿ��� ��ȭ(int,���ҽ�)")]
    public ResourceData[] needResources;

}
