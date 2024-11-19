using System.Collections.Generic;
using UnityEngine;

public class CubeInventory : MonoBehaviour
{
    public List<Item> itemsInCube;
    public GameObject inventoryUI;
    public Transform itemSlotParent;

    private void Start()
    {
        inventoryUI.SetActive(false);
    }

    public void ToggleInventoryUI()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);

        if (inventoryUI.activeSelf)
        {
            ShowInventory();
        }
    }

    private void ShowInventory()
    {
        foreach (Transform child in itemSlotParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in itemsInCube)
        {
            GameObject itemSlot = new GameObject(item.itemName);
            itemSlot.transform.SetParent(itemSlotParent);
            itemSlot.AddComponent<UnityEngine.UI.Image>().sprite = item.itemIcon; 
            itemSlot.AddComponent<UnityEngine.UI.Button>();

            // 클릭 시 아이템 설명을 출력하는 로직을 추가할 수 있습니다.
            itemSlot.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
            {
                Debug.Log($"아이템 {item.itemName}:");
            });
        }
    }
}
