using System.Collections;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour, IInteractable
{
    public GameObject cubeInventoryUI; // ť�� �κ��丮 UI
    public CubeInventory cubeInventory; // ť���� ������ ����Ʈ

    private Renderer _renderer;
    private Color _originalColor;
    private bool _isFlashing = false;

    void Start()
    {
        if (cubeInventory != null)
        {
            cubeInventory.InitializeItems();    // �ʱ� ������ �߰�
            Debug.Log("Cube Inventory Initialized. Item Count: " + cubeInventory.items.Count);
        }
    }

    public string GetInteractPrompt()
    {
        return "��ȣ�ۿ� [E]";
    }

    public void OnInteract()
    {
        OpenCubeInventory();
    }

    private void OpenCubeInventory()
    {
        if (cubeInventory != null)
        {
            // �κ��丮 UI�� Ȱ��ȭ�Ͽ� ť�� �κ��丮 �������� ������
            cubeInventoryUI.SetActive(true);

            // �κ��丮 UI�� ������ ǥ��
            DisplayItems();
        }
    }

    private void DisplayItems()
    {
        // UI�� ǥ�õ� ���� �����۵��� ���� (�ڽ� ��ü�� ����)
        foreach (Transform child in cubeInventoryUI.transform)
        {
            Destroy(child.gameObject);
        }

        // �� �������� UI�� ǥ���ϱ� ���� �۾�
        foreach (var item in cubeInventory.items)
        {
            GameObject newItemButton = new GameObject(item.itemName);
            newItemButton.AddComponent<UnityEngine.UI.Button>(); // ��ư �߰�
            TextMeshProUGUI itemText = newItemButton.AddComponent<TextMeshProUGUI>(); // �ؽ�Ʈ �߰�
            itemText.text = item.itemName;
            itemText.fontSize = 20;

            // ������ ��ư�� UI�� ��ġ
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

    // ������ ȿ���� ���� �ڷ�ƾ
    private IEnumerator FlashCoroutine()
    {
        _isFlashing = true;

        // �������� ���� ������ ���� ����
        _renderer.material.color = Color.red; // ���������� ����
        yield return new WaitForSeconds(0.2f); // ��� ��ٸ�

        _renderer.material.color = _originalColor; // ���� �������� ����
        yield return new WaitForSeconds(0.2f); // ��� ��ٸ�

        _renderer.material.color = Color.red; // ���������� �ٽ� ����
        yield return new WaitForSeconds(0.2f);

        _renderer.material.color = _originalColor; // ���� �������� ����

        _isFlashing = false;
    }
}
