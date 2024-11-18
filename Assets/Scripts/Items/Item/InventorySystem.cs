using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public int width = 5;  // 인벤토리 가로 크기
    public int height = 4;  // 인벤토리 세로 크기
    public Slot[] slots;  // 슬롯 배열
    private InventoryItem[,] inventoryGrid;  // 2D 배열로 슬롯 관리

    void Start()
    {
        inventoryGrid = new InventoryItem[width, height];
    }

    // 아이템을 인벤토리에 배치할 수 있는지 확인
    public bool CanPlaceItem(InventoryItem item, int startX, int startY)
    {
        if (startX + item.width > width || startY + item.height > height)
            return false;

        for (int x = startX; x < startX + item.width; x++)
        {
            for (int y = startY; y < startY + item.height; y++)
            {
                if (inventoryGrid[x, y] != null)
                    return false;
            }
        }
        return true;
    }

    // 아이템을 인벤토리에 배치
    public void PlaceItem(InventoryItem item, int startX, int startY)
    {
        for (int x = startX; x < startX + item.width; x++)
        {
            for (int y = startY; y < startY + item.height; y++)
            {
                inventoryGrid[x, y] = item;
            }
        }
    }
}
