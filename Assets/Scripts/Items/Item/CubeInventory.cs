using System.Collections.Generic;
using UnityEngine;

public class CubeInventory : MonoBehaviour
{
    public List<Item> items; // 아이템 리스트

    // 아이템 추가
    public void AddItem(Item newItem)
    {
        items.Add(newItem);
    }

    // 아이템 제거
    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    // 아이템 초기화 (테스트용)
    public void InitializeItems()
    {
        AddItem(new Item("아이템 1", null, 1, 1));  // 가로 1, 세로 1인 아이템
        AddItem(new Item("아이템 2", null, 2, 2));  // 가로 2, 세로 2인 아이템
        AddItem(new Item("아이템 3", null, 1, 2));  // 가로 1, 세로 2인 아이템
    }
}
