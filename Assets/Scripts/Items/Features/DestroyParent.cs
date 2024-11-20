using Defines;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    private ItemSO itemData;

    protected virtual void Awake()
    {
        itemData = ItemManager.Instance.GetItemSO(gameObject.name);

    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandleItemPickup();
            Destroy(transform.parent.gameObject);
        }
    }

    private void HandleItemPickup()
    {
        switch (gameObject.tag)
        {
            case "Gold":
                GameManager.Instance.AddGold(itemData.resourceValue);
                break;
            case "Exp":
                GameManager.Instance.AddEXP(itemData.resourceValue);
                break;
            default:
                Debug.LogWarning($"아이템 태그 없음");
                break;
        }
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
