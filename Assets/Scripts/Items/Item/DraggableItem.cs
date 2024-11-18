using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private InventorySlot currentSlot;
    private Inventory inventory;
    private Item item;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        inventory = FindObjectOfType<Inventory>();
        item = GetComponent<Item>();  // 아이템 컴포넌트에서 아이템 정보를 가져옴
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        currentSlot = GetComponentInParent<InventorySlot>();  // 현재 슬롯 정보 저장
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;  // 마우스 위치로 아이템 이동
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // 드래그 종료 시, 아이템이 배치 가능한지 확인하고 배치
        if (inventory.AddItem(item))
        {
            currentSlot.RemoveItem();  // 이전 슬롯에서 아이템 제거
        }
    }
}
