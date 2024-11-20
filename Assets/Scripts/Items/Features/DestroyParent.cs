using Defines;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    private ItemSO itemData;

    protected virtual void Awake()
    {
        itemData = ItemManager.Instance.GetResourceItemSO(gameObject.name);
        //아이템매니저에서 SO에서 ItemType 데이터를 받아오는 매서드

    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandleGoldAndExp();
            Destroy(transform.parent.gameObject);
        }
    }

    private void HandleGoldAndExp()
    {
        switch (gameObject.tag)
        {
            case "Gold":
                GameManager.Instance.AddGold(itemData.resourceValue);
                break;
            case "Exp":
                GameManager.Instance.AddEXP(itemData.resourceValue);
                break;
            case "Item":
                HandleItem();
                break;
            default:
                Debug.LogWarning($"아이템 태그 없음");
                break;
        }
    }

    private static void HandleItem()
    {
        //if(itemData == null)
        //switch (itemData.itemType)
        //{
        //    case ItemType.Consumable:
        //        //플레이어 체력회복 매서드 호출
        //        break;
        //    case ItemType.Equipable:
        //        //플레이어 무기 프리팹 교체
        //        //스탯 so데이터에서 받아서 교체
        //        break;
        //    case ItemType.Passive:
        //        //인벤토리로
        //        break;
        //    default:
        //        Debug.Log($"지정된 아이템 타입 없음");
        //        break;

        //}

    }


    //임시
    private int GetGoldValue()
    {
        return 10;
    }

    private int GetEXPValue()
    {
        return 5;
    }
    //
}
