using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs; // 3종류의 적 프리팹 배열
    public int maxEnemies = 10; // 최대 적 수
    public float spawnInterval = 5f; // 적 생성 간격
    public float spawnRadius = 10f; // 생성 반경
    public LayerMask navMeshLayerMask; // NavMesh Layer Mask

    [Header("Mini Boss Settings")]
    [Range(0f, 1f)] public float minibossSpawnChance = 0.5f; // 미니보스 생성 확률 (50%)
    public Vector3 minibossScaleMultiplier = new Vector3(2f, 2f, 2f); // 미니보스 크기 배율
    public float minibossStatMultiplier = 2f; // 미니보스 능력치 배율

    private List<GameObject> spawnedEnemies = new List<GameObject>(); // 생성된 적 리스트

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (spawnedEnemies.Count < maxEnemies)
            {
                Vector3 spawnPosition = GetRandomNavMeshPosition();
                if (spawnPosition != Vector3.zero)
                {
                    SpawnEnemyAtPosition(spawnPosition);
                }
            }

            yield return new WaitForSeconds(spawnInterval); // 지정된 간격으로 반복
        }
    }

    private Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius; // 반경 내의 랜덤 위치
        NavMeshHit hit;

        // NavMesh 위의 유효한 위치를 샘플링
        if (NavMesh.SamplePosition(randomPosition, out hit, spawnRadius, NavMesh.AllAreas))
        {
            return hit.position; // 유효한 위치 반환
        }

        return Vector3.zero; // 유효하지 않으면 Vector3.zero 반환
    }

    private void SpawnEnemyAtPosition(Vector3 position)
    {
        // 적 프리팹 랜덤 선택
        GameObject selectedPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // 적 생성
        GameObject newEnemy = Instantiate(selectedPrefab, position, Quaternion.identity);

        // 보스 생성 여부 판단
        bool isBoss = Random.value < minibossSpawnChance;

        if (isBoss)
        {
            // 보스 크기 설정
            newEnemy.transform.localScale = Vector3.Scale(newEnemy.transform.localScale, minibossScaleMultiplier);

            // EnemyDataManager를 통해 능력치 조정
            EnemyDataManager dataManager = newEnemy.GetComponent<EnemyDataManager>();
            if (dataManager != null && dataManager.enemyData != null)
            {
                // 보스 능력치 조정
                EnemyData minibossData = ScriptableObject.CreateInstance<EnemyData>();
                minibossData.startHealth = Mathf.RoundToInt(dataManager.enemyData.startHealth * minibossStatMultiplier);
                minibossData.attackDamage = Mathf.RoundToInt(dataManager.enemyData.attackDamage * minibossStatMultiplier);
                minibossData.goldValue = Mathf.RoundToInt(dataManager.enemyData.goldValue * minibossStatMultiplier);
                minibossData.expValue = Mathf.RoundToInt(dataManager.enemyData.expValue * minibossStatMultiplier);
                minibossData.disappearSpeed = dataManager.enemyData.disappearSpeed;

                // 보스 데이터를 적용
                dataManager.enemyData = minibossData;
            }

            Debug.Log($"미니보스가 생성되었습니다! {newEnemy.name}");
        }

        // 생성된 적 리스트에 추가
        spawnedEnemies.Add(newEnemy);
    }
}
