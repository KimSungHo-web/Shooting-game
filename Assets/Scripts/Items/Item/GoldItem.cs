using UnityEngine;

public class GoldItem : MonoBehaviour
{
    public int goldValue; // EnemyData�� goldValue�� �Ҵ���� ����

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // GameManager�� ���� ��� �߰�
            GameManager.Instance.AddGold(goldValue);

            // ������ �Ҹ�
            Destroy(gameObject);

            Debug.Log($"�÷��̾ {goldValue} ��带 ȹ���߽��ϴ�.");
        }
    }
}
