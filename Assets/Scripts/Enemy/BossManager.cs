using UnityEngine;

public class BossManager : MonoBehaviour
{
    [Header("Boss Settings")]
    public GameObject[] bossPrefabs; // ���� ������
    public Transform spawnPoint; // ���� ���� ��ġ
    private GameObject currentBoss; // ���� ���� �ν��Ͻ�

    public float spawnTime = 120f; // ���� ���� ��� �ð�
    private float timer;
    public bool isBossSpawned = false;

    private void Start()
    {
        timer = spawnTime;
    }

    private void Update()
    {
        if (!isBossSpawned)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                SpawnBoss();
            }
        }
    }

    private void SpawnBoss()
    {
        if (bossPrefabs.Length > 0 && spawnPoint != null)
        {
            // �������� ���� ������ ����
            int randomIndex = Random.Range(0, bossPrefabs.Length);
            GameObject selectedBossPrefab = bossPrefabs[randomIndex];

            // ���� ����
            currentBoss = Instantiate(selectedBossPrefab, spawnPoint.position, Quaternion.identity);
            isBossSpawned = true;
        }
    }

    public void OnBossDefeated()
    {
        if (currentBoss != null)
        {
            Destroy(currentBoss);
            isBossSpawned = false;
            // �߰� ���� �Ǵ� ���� �������� ���� �߰� ����
        }
    }
}
