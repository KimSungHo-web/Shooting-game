using Defines;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾���Ϸ�");
            ItemSO itemData = ItemManager.Instance.GetItemSO(ItemType.Consumable, Rarity.Common, gameObject.name);

            //�κ��丮�� ���� �ż��� ���⿡

            Destroy(this.gameObject);
        }

    }
}