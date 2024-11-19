using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs; // 3������ �� ������ �迭
    public int maxEnemies = 10; // �ִ� �� ��
    public float spawnInterval = 5f; // �� ���� ����
    public float spawnRadius = 10f; // ���� �ݰ�
    public LayerMask navMeshLayerMask; // NavMesh Layer Mask

    [Header("Mini Boss Settings")]
    [Range(0f, 1f)] public float minibossSpawnChance = 0.5f; // �̴Ϻ��� ���� Ȯ�� (50%)
    public Vector3 minibossScaleMultiplier = new Vector3(2f, 2f, 2f); // �̴Ϻ��� ũ�� ����
    public float minibossStatMultiplier = 2f; // �̴Ϻ��� �ɷ�ġ ����

    private List<GameObject> spawnedEnemies = new List<GameObject>(); // ������ �� ����Ʈ

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

            yield return new WaitForSeconds(spawnInterval); // ������ �������� �ݺ�
        }
    }

    private Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius; // �ݰ� ���� ���� ��ġ
        NavMeshHit hit;

        // NavMesh ���� ��ȿ�� ��ġ�� ���ø�
        if (NavMesh.SamplePosition(randomPosition, out hit, spawnRadius, NavMesh.AllAreas))
        {
            return hit.position; // ��ȿ�� ��ġ ��ȯ
        }

        return Vector3.zero; // ��ȿ���� ������ Vector3.zero ��ȯ
    }

    private void SpawnEnemyAtPosition(Vector3 position)
    {
        // �� ������ ���� ����
        GameObject selectedPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // �� ����
        GameObject newEnemy = Instantiate(selectedPrefab, position, Quaternion.identity);

        // ���� ���� ���� �Ǵ�
        bool isBoss = Random.value < minibossSpawnChance;

        if (isBoss)
        {
            // ���� ũ�� ����
            newEnemy.transform.localScale = Vector3.Scale(newEnemy.transform.localScale, minibossScaleMultiplier);

            // EnemyDataManager�� ���� �ɷ�ġ ����
            EnemyDataManager dataManager = newEnemy.GetComponent<EnemyDataManager>();
            if (dataManager != null && dataManager.enemyData != null)
            {
                // ���� �ɷ�ġ ����
                EnemyData minibossData = ScriptableObject.CreateInstance<EnemyData>();
                minibossData.startHealth = Mathf.RoundToInt(dataManager.enemyData.startHealth * minibossStatMultiplier);
                minibossData.attackDamage = Mathf.RoundToInt(dataManager.enemyData.attackDamage * minibossStatMultiplier);
                minibossData.goldValue = Mathf.RoundToInt(dataManager.enemyData.goldValue * minibossStatMultiplier);
                minibossData.expValue = Mathf.RoundToInt(dataManager.enemyData.expValue * minibossStatMultiplier);
                minibossData.disappearSpeed = dataManager.enemyData.disappearSpeed;

                // ���� �����͸� ����
                dataManager.enemyData = minibossData;
            }

            Debug.Log($"�̴Ϻ����� �����Ǿ����ϴ�! {newEnemy.name}");
        }

        // ������ �� ����Ʈ�� �߰�
        spawnedEnemies.Add(newEnemy);
    }
}
