using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Resource UI")]
    public TextMeshProUGUI goldText; // GoldValue에 연결
    public Image healthBar;          // HealthBar Image
    public Image staminaBar;         // StaminaBar Image
    public Image experienceBar;      // ExperienceBar Image
    public TextMeshProUGUI levelText; // Growth (레벨)에 연결

    private void Start()
    {
        // 초기 UI 상태 설정
        UpdateGold(0);       // 초기 골드: 0
        UpdateHealth(1, 1);  // 초기 체력: 1/1
        UpdateStamina(1, 1); // 초기 스태미나: 1/1
        UpdateExperience(0); // 초기 경험치: 0%
        UpdateLevel(0);      // 초기 레벨: 0
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
