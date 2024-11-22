using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO itemData;  // 아이템 데이터 (ItemSO)
    public int quantity;  // 아이템 개수 (스택 가능한 경우)

    public void Initialize(ItemSO data)
    {
        itemData = data;
        quantity = 1;  // 기본적으로 1개부터 시작
    }
}
