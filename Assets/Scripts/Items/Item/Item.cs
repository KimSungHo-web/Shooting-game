using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO itemData;  // ������ ������ (ItemSO)
    public int quantity;  // ������ ���� (���� ������ ���)

    public void Initialize(ItemSO data)
    {
        itemData = data;
        quantity = 1;  // �⺻������ 1������ ����
    }
}
