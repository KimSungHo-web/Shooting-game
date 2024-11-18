using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler
{
    public InventoryItem item;  // 드래그 중인 아이템
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private InventorySystem inventorySystem;
    private Slot currentSlot;  // 현재 슬롯에 대한 참조

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        inventorySystem = FindObjectOfType<InventorySystem>();
    }

    // 드래그 시작 시 호출되는 메서드
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // 드래그 시 투명도 변경
        canvasGroup.blocksRaycasts = false; // 드래그 중 다른 UI 요소와의 상호작용 방지
    }

    // 드래그 중에 호출되는 메서드
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // 마우스 커서를 따라가게 이동
    }

    // 드래그 종료 시 호출되는 메서드
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;  // 드래그 종료 후 투명도 원상복귀
        canvasGroup.blocksRaycasts = true;

        // 아이템이 드래그된 위치가 유효한지 확인
        if (currentSlot != null && inventorySystem.CanPlaceItem(item, currentSlot.x, currentSlot.y))
        {
            inventorySystem.PlaceItem(item, currentSlot.x, currentSlot.y);
            currentSlot.UpdateSlot(item);
        }
        else
        {
            // 배치할 수 없다면 원래 위치로 돌아가기
            //rectTransform.anchoredPosition = currentSlot.rectTransform.anchoredPosition;
        }
    }

    // 드래그된 아이템이 다른 슬롯에 드롭될 때 호출되는 메서드
    public void OnDrop(PointerEventData eventData)
    {
        Slot targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>();

        if (targetSlot != null)
        {
            currentSlot = targetSlot;
        }
    }
}
