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
        //itemImage.sprite = null;
    }

    // 아이템을 슬롯에 배치하는 메서드
    public void PlaceItem(Item item)
    {
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
