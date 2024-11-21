using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private InventorySlot currentSlot;
    private CubeInventory cubeInventory;
    private Inventory inventory;
    private Item item;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        cubeInventory = FindObjectOfType<CubeInventory>();
        item = GetComponent<Item>();  // 아이템 컴포넌트에서 아이템 데이터를 가져옴

        canvasGroup = GetComponent<CanvasGroup>();
        if (GetComponent<CanvasGroup>() == null){
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        gameObject.AddComponent<RectTransform>();
    }

    // 드래그 시작 시
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Started");
        canvasGroup.blocksRaycasts = false;
        currentSlot = GetComponentInParent<InventorySlot>();  // 현재 슬롯 정보 저장
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;  // 마우스 위치로 아이템 이동
    }

    // 드래그 종료 시
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Ended");

        canvasGroup.blocksRaycasts = true;

        if (inventory == null)
        {
            Debug.LogError("Inventory is null in OnEndDrag!");
            return;
        }

        if (item == null)
        {
            Debug.LogError("Item is null in OnEndDrag!");
            return;
        }

        // 인벤토리에 아이템을 배치할 수 있으면 배치하고 큐브 인벤토리에서 제거
        if (inventory.AddItem(item.itemData))
        {
            cubeInventory.itemsInCube.Remove(item.itemData);  // 큐브 인벤토리에서 아이템 제거
        }
        else
        {

        }
    }
}
