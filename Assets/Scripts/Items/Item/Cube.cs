using System.Collections;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour, IInteractable
{
    public GameObject cubeInventoryUI; // 큐브 인벤토리 UI
    public CubeInventory cubeInventory; // 큐브의 아이템 리스트

    private Renderer _renderer;
    private Color _originalColor;
    private bool _isFlashing = false;

    void Start()
    {
        if (cubeInventory != null)
        {
            cubeInventory.InitializeItems();    // 초기 아이템 추가
            Debug.Log("Cube Inventory Initialized. Item Count: " + cubeInventory.items.Count);
        }
    }

    public string GetInteractPrompt()
    {
        return "상호작용 [E]";
    }

    public void OnInteract()
    {
        OpenCubeInventory();
    }

    private void OpenCubeInventory()
    {
        if (cubeInventory != null)
        {
            // 인벤토리 UI를 활성화하여 큐브 인벤토리 아이템을 보여줌
            cubeInventoryUI.SetActive(true);

            // 인벤토리 UI에 아이템 표시
            DisplayItems();
        }
    }

    private void DisplayItems()
    {
        // UI에 표시된 기존 아이템들을 제거 (자식 객체들 삭제)
        foreach (Transform child in cubeInventoryUI.transform)
        {
            Destroy(child.gameObject);
        }

        // 각 아이템을 UI에 표시하기 위한 작업
        foreach (var item in cubeInventory.items)
        {
            GameObject newItemButton = new GameObject(item.itemName);
            newItemButton.AddComponent<UnityEngine.UI.Button>(); // 버튼 추가
            TextMeshProUGUI itemText = newItemButton.AddComponent<TextMeshProUGUI>(); // 텍스트 추가
            itemText.text = item.itemName;
            itemText.fontSize = 20;

            // 아이템 버튼을 UI에 배치
            newItemButton.transform.SetParent(cubeInventoryUI.transform);
        }
    }

    public void Flash()
    {
        if (_renderer != null && !_isFlashing)
        {
            StartCoroutine(FlashCoroutine());
        }
    }

    // 깜빡임 효과를 위한 코루틴
    private IEnumerator FlashCoroutine()
    {
        _isFlashing = true;

        // 깜빡임을 위해 색상을 빨리 변경
        _renderer.material.color = Color.red; // 빨간색으로 변경
        yield return new WaitForSeconds(0.2f); // 잠시 기다림

        _renderer.material.color = _originalColor; // 원래 색상으로 복원
        yield return new WaitForSeconds(0.2f); // 잠시 기다림

        _renderer.material.color = Color.red; // 빨간색으로 다시 변경
        yield return new WaitForSeconds(0.2f);

        _renderer.material.color = _originalColor; // 원래 색상으로 복원

        _isFlashing = false;
    }
}
