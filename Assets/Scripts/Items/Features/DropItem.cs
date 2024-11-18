using Defines;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [Header("드롭 테이블")]
    public ItemDroptableSO dropTable;

    //테스트용
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
        List<ItemDroptableSO.DropItem> dropItems = dropTable.commonItems;

        foreach (var dropItem in dropItems)
        {
            if (Random.Range(0f, 100f) <= dropItem.dropChance)
            {
                GameObject droppedItem = Instantiate(dropItem.item.dropItemPrefab, transform.position, Quaternion.identity);
                DropAnimation dropanim =// droppedItem.GetComponent<DropAnimation>();
                 droppedItem.AddComponent<DropAnimation>();
            
                dropanim.StartDropAnimation(transform.position);
               
                Debug.Log($"{dropItem.item}이(가) 드롭되었습니다.");
                break; 
            }
        }
    }

}
