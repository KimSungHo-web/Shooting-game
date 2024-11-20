using UnityEngine;
using static ItemDroptableSO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Management")]
    public PlayerHealthSystem playerHealth;
    public ThridPersonMovement playerMovement;

    [Header("Game Stats")]
    public int currentGold;
    public int currentEXP;
    public int currentLevel;

    [Header("UI Management")]
    public PlayerUI playerUI;

    [Header("Item Management")]
    private DropItem dropitem; 

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

    private void Start()
    {
        // 초기 UI 업데이트
        UpdateAllUI();
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        playerUI.UpdateGold(currentGold); // 골드 UI 업데이트
        Debug.Log($"골드 추가: {amount}. 현재 골드: {currentGold}");
    }

    public void AddEXP(int amount)
    {
        currentEXP += amount;

        // 현재 레벨에서 필요한 경험치 계산
        int expToNextLevel = CalculateExpForNextLevel(currentLevel);

        // 경험치가 다음 레벨에 도달하면 레벨 업
        while (currentEXP >= expToNextLevel)
        {
            currentEXP -= expToNextLevel; // 남은 경험치에서 현재 레벨에 필요한 경험치를 뺌
            LevelUp(); // 레벨 업
            expToNextLevel = CalculateExpForNextLevel(currentLevel); // 새로운 레벨의 필요 경험치 계산
        }

        // 경험치 UI 업데이트
        float expPercentage = (float)currentEXP / expToNextLevel;
        playerUI.UpdateExperience(expPercentage);

        Debug.Log($"경험치 추가: {amount}. 현재 경험치: {currentEXP}/{expToNextLevel}");
    }

    private void LevelUp()
    {
        currentLevel++;
        playerUI.UpdateLevel(currentLevel); // 레벨 UI 업데이트
        Debug.Log($"레벨 업! 현재 레벨: {currentLevel}");
    }

    public void OnPlayerDeath()
    {
        Debug.Log("플레이어가 사망했습니다. 게임 오버!");
        // 게임 오버 처리 로직
    }

    public void OnEnemyDeath(EnemyData enemyData, GameObject enemyObject)
    {
        DropItem dropItem = enemyObject.GetComponent<DropItem>();
        if (dropItem)
        {
            dropItem.Drop();
        }
        //AddGold(enemyData.goldValue);
        //AddEXP(enemyData.expValue);
        //Debug.Log($"적 사망: {enemyData.name}. 보상 - 골드: {enemyData.goldValue}, 경험치: {enemyData.expValue}");
    }

    private int CalculateExpForNextLevel(int level)
    {
        // 레벨에 따라 필요한 경험치를 계산 (임의 로직)
        return 100 + (level * 50);
    }

    private void UpdateAllUI()
    {
        playerUI.UpdateGold(currentGold);
        playerUI.UpdateHealth(playerHealth.health, playerHealth.maxHealth);
        playerUI.UpdateStamina(1, 1); // 초기 스태미나 (임의 값)
        playerUI.UpdateExperience(0); // 초기 경험치 (0%)
        playerUI.UpdateLevel(currentLevel);
    }
}
