using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryUI;
    public GameObject slotPrefab;
    public GameObject inventoryPanel;

    private int _row = 5;
    private int _col = 6;

    private InventorySlot[,] gridSlots;  // 그리드 슬롯 배열
    private List<Item> items = new List<Item>();  // 인벤토리 아이템 리스트

    void Start()
    {
        gridSlots = new InventorySlot[_row, _col];
        InitializeInventory();
        InventoryUI.SetActive(false);
        // Inventory Event register
    }

    void InitializeInventory()
    {
        for (int row = 0; row < _row; row++)
        {
            for (int col = 0; col < _col; col++)
            {
                GameObject slot = Instantiate(slotPrefab, inventoryPanel.transform);
                InventorySlot slotScript = slot.GetComponent<InventorySlot>();
                slotScript.InitializeSlot(row, col);
                gridSlots[row, col] = slotScript;  // 슬롯을 그리드에 할당
            }
        }
    }

    public void Toggle() => InventoryUI.SetActive(!InventoryUI.activeInHierarchy);

    // 아이템 배치 메서드
    public bool AddItem(Item item)
    {
        for (int row = 0; row < _row; row++)
        {
            for (int col = 0; col < _col; col++)
            {
                // 아이템이 배치 가능한 공간을 찾는다.
                if (CanPlaceItem(item, row, col))
                {
                    PlaceItem(item, row, col);
                    return true;  // 아이템 배치 성공
                }
            }
        }
        return false;  // 배치할 공간이 없음
    }

    // 아이템을 배치할 수 있는지 확인하는 메서드
    private bool CanPlaceItem(Item item, int startRow, int startCol)
    {
        if (startRow + item.height > _row || startCol + item.width > _col)
            return false;

        for (int row = startRow; row < startRow + item.height; row++)
        {
            for (int col = startCol; col < startCol + item.width; col++)
            {
                if (gridSlots[row, col].IsOccupied)
                    return false;  // 이미 아이템이 배치된 슬롯이 있으면 안 됨
            }
        }

        return true;  // 배치 가능한 경우
    }

    // 아이템을 그리드에 배치하는 메서드
    private void PlaceItem(Item item, int startRow, int startCol)
    {
        for (int row = startRow; row < startRow + item.height; row++)
        {
            for (int col = startCol; col < startCol + item.width; col++)
            {
                gridSlots[row, col].PlaceItem(item);  // 해당 슬롯에 아이템 배치
            }
        }
    }
}
