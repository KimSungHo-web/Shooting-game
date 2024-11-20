using UnityEngine;

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
        // �ʱ� UI ������Ʈ
        UpdateAllUI();
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        playerUI.UpdateGold(currentGold); // ��� UI ������Ʈ
        Debug.Log($"��� �߰�: {amount}. ���� ���: {currentGold}");
    }

    public void AddEXP(int amount)
    {
        currentEXP += amount;

        // ���� �������� �ʿ��� ����ġ ���
        int expToNextLevel = CalculateExpForNextLevel(currentLevel);

        // ����ġ�� ���� ������ �����ϸ� ���� ��
        while (currentEXP >= expToNextLevel)
        {
            currentEXP -= expToNextLevel; // ���� ����ġ���� ���� ������ �ʿ��� ����ġ�� ��
            LevelUp(); // ���� ��
            expToNextLevel = CalculateExpForNextLevel(currentLevel); // ���ο� ������ �ʿ� ����ġ ���
        }

        // ����ġ UI ������Ʈ
        float expPercentage = (float)currentEXP / expToNextLevel;
        playerUI.UpdateExperience(expPercentage);

        Debug.Log($"����ġ �߰�: {amount}. ���� ����ġ: {currentEXP}/{expToNextLevel}");
    }

    private void LevelUp()
    {
        currentLevel++;
        playerUI.UpdateLevel(currentLevel); // ���� UI ������Ʈ
        Debug.Log($"���� ��! ���� ����: {currentLevel}");
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

    private int CalculateExpForNextLevel(int level)
    {
        // ������ ���� �ʿ��� ����ġ�� ��� (���� ����)
        return 100 + (level * 50);
    }

    private void UpdateAllUI()
    {
        playerUI.UpdateGold(currentGold);
        playerUI.UpdateHealth(playerHealth.health, playerHealth.maxHealth);
        playerUI.UpdateStamina(1, 1); // �ʱ� ���¹̳� (���� ��)
        playerUI.UpdateExperience(0); // �ʱ� ����ġ (0%)
        playerUI.UpdateLevel(currentLevel);
    }
}
