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
        Debug.Log($"��� �߰�: {amount}. ���� ���: {currentGold}");
    }

    public void AddEXP(int amount)
    {
        currentEXP += amount;
        Debug.Log($"����ġ �߰�: {amount}. ���� ����ġ: {currentEXP}");
    }

    public void OnPlayerDeath()
    {
        Debug.Log("�÷��̾ ����߽��ϴ�. ���� ����!");
        // ���� ���� ó�� ����
    }

    public void OnEnemyDeath(EnemyData enemyData)
    {
        AddGold(enemyData.goldValue);
        AddEXP(enemyData.expValue);
        Debug.Log($"�� ���: {enemyData.name}. ���� - ���: {enemyData.goldValue}, ����ġ: {enemyData.expValue}");
    }
}
