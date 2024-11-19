using Defines;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "S0_ItemData", menuName = "Datas/S0_ItemData")]
public class ItemSO : InteractableSO
{
    
    public Sprite itemIcon;
    public ItemType itemType;

    [Header("�κ��丮����")]
    public bool isStackable;
    public int stackSize;
    public int SlotSize;

    [Header("���Ÿ��")]
    public EquipType equipType;
    public GameObject equipPrefab;
    public GameObject dropItemPrefab;
    public Rarity rarity;
    public bool LeftHand;
    public string childPath;

    [Header("��������(speed�� -�� �ӵ� ������)")]
    public float projectileDamage;
    public float projectileSpeed;
    public float projectileScale;

    [Header("�Һ�Ÿ��")]
    public List<ConsumableData> consumables;
    public GameObject dropConsumeItemPrefab;

    [Header("���ҽ�Ÿ��")]
    public ResourceType resourceType;
    public int resourceValue;
    public GameObject ResourcePrefab;

    [Header("ȹ�濡 �ʿ��� ��ȭ(int,���ҽ�)")]
    public ResourceData[] needResources;

}
