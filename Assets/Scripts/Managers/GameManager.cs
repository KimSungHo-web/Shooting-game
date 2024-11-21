using UnityEngine;
using static ItemDroptableSO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Management")]
    public PlayerHealthSystem playerHealth;
    public ThirdPersonMovement playerMovement;

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
        UpdateAllUI();

    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        playerUI.UpdateGold(currentGold);
       // Debug.Log($"���?�߰�: {amount}. ���� ���? {currentGold}");
    }

    public void AddEXP(int amount)
    {
        currentEXP += amount;

        
        int expToNextLevel = CalculateExpForNextLevel(currentLevel);

       
        while (currentEXP >= expToNextLevel)
        {
            currentEXP -= expToNextLevel; 
            LevelUp(); 
            expToNextLevel = CalculateExpForNextLevel(currentLevel);
        }

        float expPercentage = (float)currentEXP / expToNextLevel;
        playerUI.UpdateExperience(expPercentage);

       // Debug.Log($"����ġ �߰�: {amount}. ���� ����ġ: {currentEXP}/{expToNextLevel}");
    }

    private void LevelUp()
    {
        currentLevel++;
        playerUI.UpdateLevel(currentLevel);
      //  Debug.Log($"���� ��! ���� ����: {currentLevel}");
    }

    public void OnPlayerDeath()
    {
       // Debug.Log("�÷��̾ ����߽��ϴ�? ���� ����!");
  
    }

    public void OnEnemyDeath(EnemyData enemyData, GameObject enemyObject)
    {

        DropItem dropItem = enemyObject.GetComponent<DropItem>();
        if (dropItem)
        {
            dropItem.Drop();
        }

        AddGold(enemyData.goldValue);
        AddEXP(enemyData.expValue);
      //  Debug.Log($"�� ���? {enemyData.name}. ���� - ���? {enemyData.goldValue}, ����ġ: {enemyData.expValue}");

    }

    private int CalculateExpForNextLevel(int level)
    {
        return 100 + (level * 50);
    }

    private void UpdateAllUI()
    {
        playerUI.UpdateGold(currentGold);
        playerUI.UpdateHealth(playerHealth.health, playerHealth.maxHealth);
        playerUI.UpdateStamina(1, 1); 
        playerUI.UpdateExperience(0); 
        playerUI.UpdateLevel(currentLevel);
    }
}
