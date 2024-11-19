using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Management")]
    public PlayerHealthSystem playerHealth;
    public ThridPersonMovement playerMovement;

    [Header("Game Stats")]
    public int currentGold;
    public int currentEXP;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        Debug.Log($"골드 추가: {amount}. 현재 골드: {currentGold}");
    }

    public void AddEXP(int amount)
    {
        currentEXP += amount;
        Debug.Log($"경험치 추가: {amount}. 현재 경험치: {currentEXP}");
    }

    public void OnPlayerDeath()
    {
        Debug.Log("플레이어가 사망했습니다. 게임 오버!");
        // 게임 오버 처리 로직
    }

    public void OnEnemyDeath(EnemyData enemyData)
    {
        AddGold(enemyData.goldValue);
        AddEXP(enemyData.expValue);
        Debug.Log($"적 사망: {enemyData.name}. 보상 - 골드: {enemyData.goldValue}, 경험치: {enemyData.expValue}");
    }
}
