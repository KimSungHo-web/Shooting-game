using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private InventorySlot currentSlot;
    private CubeInventory cubeInventory;
    private Inventory inventory;
    private ItemSO item;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        inventory = FindObjectOfType<Inventory>();
        cubeInventory = FindObjectOfType<CubeInventory>();
        item = GetComponent<ItemSO>();  // 아이템 컴포넌트에서 아이템 정보를 가져옴
    }

    // 드래그 시작 시
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        currentSlot = GetComponentInParent<InventorySlot>();  // 현재 슬롯 정보 저장
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;  // 마우스 위치로 아이템 이동
    }

    // 드래그 종료 시
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // 인벤토리에 아이템을 배치할 수 있으면 배치하고 큐브 인벤토리에서 제거
        if (inventory.AddItem(item))
        {
            cubeInventory.itemsInCube.Remove(item);  // 큐브 인벤토리에서 아이템 제거
        }
        else
        {
            // 배치할 공간이 없다면 큐브 인벤토리로 되돌리기
            cubeInventory.itemsInCube.Add(item);
        }
    }

    public void Initialize(ItemSO newItem)
    {
        item = newItem;
    }
}
