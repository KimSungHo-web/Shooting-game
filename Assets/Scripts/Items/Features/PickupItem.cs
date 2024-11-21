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
            Debug.Log($"아이템 데이터가 성공적으로 할당되었습니다: {itemData.name}");
        }
        else
        {
            Debug.LogWarning("아이템 데이터 할당 실패: null 값");
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
                // 큐브 인벤토리에 아이템 추가
                Debug.Log(itemData);
                cubeInventory.AddItemToCube(itemData);
            }

            AudioManager.Instance.PlayItemDropSFX();
            Destroy(this.gameObject);
        }

    }
}
