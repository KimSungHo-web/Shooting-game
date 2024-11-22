
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BossUI : MonoBehaviour
{
    public Image healthBar; // 체력을 나타낼 이미지
    public TMP_Text bossNameText; // 보스 이름 표시
    private GameObject boss; // 현재 보스

    private void Start()
    {
        // 초기 UI 상태 설정

        UpdateBossHealth(1, 1);  // 초기 체력: 1/1

    }
    public void SetBoss(GameObject newBoss, string bossName, int maxHealth)
    {
        boss = newBoss;
        bossNameText.text = bossName;
        healthBar.fillAmount = 1; // 최대 체력에서 시작

        // UI 활성화
        gameObject.SetActive(true);
    }

    public void UpdateBossHealth(int currentHealth, int maxHealth)
    {
        if (boss != null)
        {
            float fillAmount = (float)currentHealth / maxHealth;
            healthBar.fillAmount = fillAmount; // 현재 체력 비율로 Fill Amount 설정
        }
    }

    public void HideUI()
    {
        // UI 비활성화
        gameObject.SetActive(false);
    }
}
