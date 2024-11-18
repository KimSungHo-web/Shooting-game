using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int x, y;  // 슬롯의 위치
    public Image slotImage;  // 슬롯에 아이템을 표시할 UI 이미지
    private InventoryItem storedItem;

    public void UpdateSlot(InventoryItem item)
    {
        storedItem = item;
        slotImage.sprite = item.itemSprite;  // 슬롯에 아이템 이미지 표시
    }

    public void ClearSlot()
    {
        storedItem = null;
        slotImage.sprite = null;  // 슬롯 비우기
    }
}
