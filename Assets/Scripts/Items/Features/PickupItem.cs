using Defines;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어감지완료");
            ItemSO itemData = ItemManager.Instance.GetItemSO(ItemType.Consumable, Rarity.Common, gameObject.name);

            //인벤토리로 가는 매서드 여기에

            Destroy(this.gameObject);
        }

    }
}
