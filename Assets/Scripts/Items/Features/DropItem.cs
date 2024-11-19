using Defines;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static ItemDroptableSO;

public class DropItem : MonoBehaviour
{
    [Header("드롭 테이블")]
    public ItemDroptableSO dropTable;

    [Header("몬스터 종류")]
    public MonsterType monsterType;


    private void Start()
    {
       
    }
    public void OnDeath()
    {
        Drop();
        DropGoldAndExp();
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
               
                Debug.Log($"{dropItem.item}이(가) 드롭되었습니다.");
                break; 
            }
        }
    }

    private void DropGoldAndExp()
    {
        int goldAmount = 0;
        int expAmount = 0;
        GameObject droppedExp = null;
        Vector3 dropPosition;

        switch (monsterType)
        {
            case MonsterType.Small:
                goldAmount = Random.Range(1, 5);
                expAmount = Random.Range(1, 3);
                droppedExp = ItemManager.Instance.Instantiate("ExpS");
                break;
            case MonsterType.Medium:
                goldAmount = Random.Range(5, 15);
                expAmount = Random.Range(1, 2);
                droppedExp = ItemManager.Instance.Instantiate("ExpM");
                break;
            case MonsterType.Large:
                goldAmount = Random.Range(15, 30);
                expAmount = 1;
                droppedExp = ItemManager.Instance.Instantiate("ExpL");

                break;
        }

        if (droppedExp != null)
        {
            dropPosition = transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            droppedExp.transform.position = dropPosition;
        }

        for (int i = 0; i < goldAmount; i++)
        {
            dropPosition = transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            GameObject droppedGold = ItemManager.Instance.Instantiate("Gold");
            if (droppedGold != null)
            {
                droppedGold.transform.position = dropPosition;

                DropAnimation dropAnim = droppedGold.AddComponent<DropAnimation>();
                dropAnim.StartDropAnimation(dropPosition,Rarity.Uncommon);
            }
        }

    }

}
