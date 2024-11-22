
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BossUI : MonoBehaviour
{
    public Image healthBar; // ü���� ��Ÿ�� �̹���
    public TMP_Text bossNameText; // ���� �̸� ǥ��
    private GameObject boss; // ���� ����

    private void Start()
    {
        // �ʱ� UI ���� ����

        UpdateBossHealth(1, 1);  // �ʱ� ü��: 1/1

    }
    public void SetBoss(GameObject newBoss, string bossName, int maxHealth)
    {
        boss = newBoss;
        bossNameText.text = bossName;
        healthBar.fillAmount = 1; // �ִ� ü�¿��� ����

        // UI Ȱ��ȭ
        gameObject.SetActive(true);
    }

    public void UpdateBossHealth(int currentHealth, int maxHealth)
    {
        if (boss != null)
        {
            float fillAmount = (float)currentHealth / maxHealth;
            healthBar.fillAmount = fillAmount; // ���� ü�� ������ Fill Amount ����
        }
    }

    public void HideUI()
    {
        // UI ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
