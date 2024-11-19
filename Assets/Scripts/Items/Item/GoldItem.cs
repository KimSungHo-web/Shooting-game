using UnityEngine;

public class GoldItem : MonoBehaviour
{
    public int goldValue; // EnemyData의 goldValue를 할당받을 변수

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // GameManager를 통해 골드 추가
            GameManager.Instance.AddGold(goldValue);

            // 아이템 소멸
            Destroy(gameObject);

            Debug.Log($"플레이어가 {goldValue} 골드를 획득했습니다.");
        }
    }
}
