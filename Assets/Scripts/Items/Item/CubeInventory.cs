using System.Collections.Generic;
using UnityEngine;

public class CubeInventory : MonoBehaviour
{
    public List<ItemSO> itemsInCube;
    public GameObject inventoryUI;
    public Transform itemSlotParent;
    public GameObject itemSlotPrefab;

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

    public void AddItemToCube(ItemSO itemData)
    {
        GameObject itemObject = new GameObject(itemData.displayName);
        Item item = itemObject.AddComponent<Item>();

        item.Initialize(itemData);
        itemsInCube.Add(itemData);

        ShowInventory();  // 인벤토리 UI 업데이트
    }

    private void ShowInventory()
    {
        foreach (var item in itemsInCube)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, itemSlotParent);
            itemSlot.GetComponent<UnityEngine.UI.Image>().sprite = item.itemIcon;

            itemSlot.AddComponent<Item>();
            itemSlot.GetComponent<Item>().itemData = item;

            UnityEngine.UI.Button itemButton = itemSlot.GetComponent<UnityEngine.UI.Button>();
            itemButton.onClick.AddListener(() =>
            {
                Debug.Log($"아이템 {item.displayName} 선택됨");
            });
        }
    }
}
