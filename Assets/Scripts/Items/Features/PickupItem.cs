using Defines;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public CubeInventory cubeInventory;
    private ItemSO itemData;

    public void SetItemData(ItemSO data)
    {
        itemData = data;

        if (itemData != null)
        {
            Debug.Log($"������ �����Ͱ� ���������� �Ҵ�Ǿ����ϴ�: {itemData.name}");
        }
        else
        {
            Debug.LogWarning("������ ������ �Ҵ� ����: null ��");
        }
    }

    private void Awake()
    {
        cubeInventory = FindObjectOfType<CubeInventory>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (itemData != null)
            {
                // ť�� �κ��丮�� ������ �߰�
                Debug.Log(itemData);
                cubeInventory.AddItemToCube(itemData);
            }

            AudioManager.Instance.PlayItemDropSFX();
            Destroy(this.gameObject);
        }

    }
}
