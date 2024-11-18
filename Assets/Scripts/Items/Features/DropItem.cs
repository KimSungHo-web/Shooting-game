using Defines;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [Header("��� ���̺�")]
    public ItemDroptableSO dropTable;

    //�׽�Ʈ��
    private void Start()
    {
        Drop();
    }
    public void OnDeath()
    {
        Drop();
    }

    private void Drop()
    {
        List<ItemDroptableSO.DropItem> dropItems = new List<ItemDroptableSO.DropItem>();

        dropItems.AddRange(dropTable.commonItems);
        dropItems.AddRange(dropTable.uncommonItems);
        dropItems.AddRange(dropTable.rareItems);
        dropItems.AddRange(dropTable.epicItems);
        dropItems.AddRange(dropTable.legendaryItems);

        foreach (var dropItem in dropItems)
        {
            if (Random.Range(0f, 100f) <= dropItem.dropChance)
            {
                GameObject droppedItem = Instantiate(dropItem.item.dropItemPrefab, transform.position, Quaternion.identity);
                DropAnimation dropanim =// droppedItem.GetComponent<DropAnimation>();
                 droppedItem.AddComponent<DropAnimation>();
            
                dropanim.StartDropAnimation(transform.position,dropItem.item.rarity);
               
                Debug.Log($"{dropItem.item}��(��) ��ӵǾ����ϴ�.");
                break; 
            }
        }
    }

}
