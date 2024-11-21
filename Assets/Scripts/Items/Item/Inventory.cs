using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemSO> itemsInInven;
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

    public void AddItemToInven(ItemSO itemData)
    {
        //GameObject itemObject = new GameObject(itemData.displayName);
        //Item item = itemObject.AddComponent<Item>();

        //item.Initialize(itemData);
        itemsInInven.Add(itemData);

        ShowInventory();  // 인벤토리 UI 업데이트
    }

    public void ShowInventory()
    {
        foreach (Transform child in itemSlotParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in itemsInInven)
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
