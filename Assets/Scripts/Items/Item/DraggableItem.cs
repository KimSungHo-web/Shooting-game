using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private InventorySlot currentSlot;
    private CubeInventory cubeInventory;
    private Inventory inventory;
    private Item item;

    private Vector3 originalPosition;  // 아이템이 원래 있던 위치를 저장할 변수

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        cubeInventory = FindObjectOfType<CubeInventory>();
        item = GetComponent<Item>();  // 아이템 컴포넌트에서 아이템 데이터를 가져옴

        canvasGroup = GetComponent<CanvasGroup>();
        if (GetComponent<CanvasGroup>() == null){
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
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

        InventorySlot targetSlot = GetTargetSlot(eventData.position);

        if (targetSlot != null)
        {
            // 목표 슬롯이 인벤토리 슬롯이라면, 현재 아이템의 슬롯을 기준으로 이동
            if (targetSlot.GetComponentInParent<Inventory>())
            {
                if (cubeInventory.itemsInCube.Contains(item.itemData))
                {
                    cubeInventory.itemsInCube.Remove(item.itemData);
                    inventory.itemsInInven.Add(item.itemData);
                }
            }
            else if (targetSlot.GetComponentInParent<CubeInventory>())
            {
                if (inventory.itemsInInven.Contains(item.itemData))
                {
                    inventory.itemsInInven.Remove(item.itemData);
                    cubeInventory.itemsInCube.Add(item.itemData);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }

        ShowInventory();  // UI 업데이트
    }

    // 드래그 종료 후 목표 슬롯을 찾는 메서드
    private InventorySlot GetTargetSlot(Vector2 pointerPosition)
    {
        // 화면의 좌표를 RectTransform으로 변환하여, 그 좌표가 InventorySlot 안에 있는지 확인
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pointerPosition), Vector2.zero);
        if (hit.collider != null)
        {
            // 충돌한 객체가 InventorySlot이라면 해당 슬롯을 반환
            return hit.collider.GetComponent<InventorySlot>();
        }
        return null;  // 슬롯 외의 곳이면 null 반환
    }

    // UI 업데이트 메서드 (인벤토리 UI 갱신)
    private void ShowInventory()
    {
        inventory.ShowInventory();
        cubeInventory.ShowInventory();
    }
}
