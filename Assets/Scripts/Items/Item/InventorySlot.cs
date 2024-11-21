using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemImage;
    public int row, col;  // 슬롯의 위치
    private Item currentItem;
    public bool IsOccupied => currentItem != null;  // 슬롯이 아이템을 차지하고 있는지 여부

    public void InitializeSlot(int row, int col)
    {
        this.row = row;
        this.col = col;

        // itemImage를 자식 오브젝트에서 찾기
        if (itemImage == null)
        {
            itemImage = GetComponentInChildren<Image>();

            if (itemImage == null)
            {
                Debug.LogError("InventorySlot에서 itemImage가 초기화되지 않았습니다. 이미지 컴포넌트를 찾을 수 없습니다.");
            }
        }

        // 슬롯을 초기화했을 때 이미지를 비웁니다.
        itemImage.sprite = null;
    }

    // 아이템을 슬롯에 배치하는 메서드
    public void PlaceItem(Item item)
    {
        if (item == null || item.itemData == null)
        {
            Debug.LogError("PlaceItem() 호출 시 item 또는 itemData가 null입니다.");
            return;
        }

        if (itemImage == null)
        {
            Debug.LogError("InventorySlot에서 itemImage가 초기화되지 않았습니다.");
            return;
        }

        currentItem = item;
        itemImage.sprite = item.itemData.itemIcon;
    }

    // 슬롯에서 아이템을 제거하는 메서드
    public void RemoveItem()
    {
        currentItem = null;
        itemImage.sprite = null;
    }
}
