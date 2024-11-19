using UnityEngine;

public class BossManager : MonoBehaviour
{
    [Header("Boss Settings")]
    public GameObject[] bossPrefabs; // 보스 프리팹
    public Transform spawnPoint; // 보스 스폰 위치
    private GameObject currentBoss; // 현재 보스 인스턴스

    public float spawnTime = 120f; // 보스 생성 대기 시간
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
            // 랜덤으로 보스 프리팹 선택
            int randomIndex = Random.Range(0, bossPrefabs.Length);
            GameObject selectedBossPrefab = bossPrefabs[randomIndex];

            // 보스 생성
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
            // 추가 보상 또는 다음 스테이지 로직 추가 가능
        }
    }
}
