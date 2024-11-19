using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Resource UI")]
    public TextMeshProUGUI goldText;        // GoldValue에 연결
    public Image healthBar;                 // HealthBar Image
    public Image staminaBar;                // StaminaBar Image
    public Image experienceBar;             // ExperienceBar Image
    public TextMeshProUGUI levelText;       // Growth (레벨)에 연결
    public TextMeshProUGUI messageText;     // 목표
    public TextMeshProUGUI timerText;       // 플레이어 전체 게임 타이머

    private float elapsedTime; // 전체 경과 시간 저장

    private void Start()
    {
        // 초기 UI 상태 설정
        UpdateGold(0);       // 초기 골드: 0
        UpdateHealth(1, 1);  // 초기 체력: 1/1
        UpdateStamina(1, 1); // 초기 스태미나: 1/1
        UpdateExperience(0); // 초기 경험치: 0%
        UpdateLevel(0);      // 초기 레벨: 0
        UpdateMessage("Survive");
        elapsedTime = 0f;    // 게임 시작 시간 초기화
    }

    private void Update()
    {
        // 전체 플레이 시간 증가
        elapsedTime += Time.deltaTime;

        // 전체 경과 시간을 UI에 업데이트
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
            int hours = Mathf.FloorToInt(time / 3600); // 시간 계산
            int minutes = Mathf.FloorToInt((time % 3600) / 60); // 분 계산
            int seconds = Mathf.FloorToInt(time % 60); // 초 계산
            timerText.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}"; // HH:MM:SS 형식으로 출력
        }
    }

    // 골드 UI 업데이트
    public void UpdateGold(int gold)
    {
        goldText.text = $"{gold}";
    }

    // 체력 UI 업데이트
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        float fillAmount = (float)currentHealth / maxHealth;
        healthBar.fillAmount = fillAmount;
    }

    // 스태미나 UI 업데이트
    public void UpdateStamina(int currentStamina, int maxStamina)
    {
        float fillAmount = (float)currentStamina / maxStamina;
        staminaBar.fillAmount = fillAmount;
    }

    // 경험치 UI 업데이트
    public void UpdateExperience(float percentage)
    {
        experienceBar.fillAmount = percentage;
    }

    // 레벨 UI 업데이트
    public void UpdateLevel(int level)
    {
        levelText.text = $"Lv. {level:D2}";
    }
}
