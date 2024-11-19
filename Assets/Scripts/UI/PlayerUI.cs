using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Resource UI")]
    public TextMeshProUGUI goldText;        // GoldValue�� ����
    public Image healthBar;                 // HealthBar Image
    public Image staminaBar;                // StaminaBar Image
    public Image experienceBar;             // ExperienceBar Image
    public TextMeshProUGUI levelText;       // Growth (����)�� ����
    public TextMeshProUGUI messageText;     // ��ǥ
    public TextMeshProUGUI timerText;       // �÷��̾� ��ü ���� Ÿ�̸�

    private float elapsedTime; // ��ü ��� �ð� ����

    private void Start()
    {
        // �ʱ� UI ���� ����
        UpdateGold(0);       // �ʱ� ���: 0
        UpdateHealth(1, 1);  // �ʱ� ü��: 1/1
        UpdateStamina(1, 1); // �ʱ� ���¹̳�: 1/1
        UpdateExperience(0); // �ʱ� ����ġ: 0%
        UpdateLevel(0);      // �ʱ� ����: 0
        UpdateMessage("Survive");
        elapsedTime = 0f;    // ���� ���� �ð� �ʱ�ȭ
    }

    private void Update()
    {
        // ��ü �÷��� �ð� ����
        elapsedTime += Time.deltaTime;

        // ��ü ��� �ð��� UI�� ������Ʈ
        UpdateTimer(elapsedTime);
    }

    private void UpdateMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
    }

    private void UpdateTimer(float time)
    {
        if (timerText != null)
        {
            int hours = Mathf.FloorToInt(time / 3600); // �ð� ���
            int minutes = Mathf.FloorToInt((time % 3600) / 60); // �� ���
            int seconds = Mathf.FloorToInt(time % 60); // �� ���
            timerText.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}"; // HH:MM:SS �������� ���
        }
    }

    // ��� UI ������Ʈ
    public void UpdateGold(int gold)
    {
        goldText.text = $"{gold}";
    }

    // ü�� UI ������Ʈ
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        float fillAmount = (float)currentHealth / maxHealth;
        healthBar.fillAmount = fillAmount;
    }

    // ���¹̳� UI ������Ʈ
    public void UpdateStamina(int currentStamina, int maxStamina)
    {
        float fillAmount = (float)currentStamina / maxStamina;
        staminaBar.fillAmount = fillAmount;
    }

    // ����ġ UI ������Ʈ
    public void UpdateExperience(float percentage)
    {
        experienceBar.fillAmount = percentage;
    }

    // ���� UI ������Ʈ
    public void UpdateLevel(int level)
    {
        levelText.text = $"Lv. {level:D2}";
    }
}
