using System.Collections;
using UnityEngine;
using Cinemachine;

public class BossManager : MonoBehaviour
{
    [Header("Boss Settings")]
    public GameObject[] bossPrefabs; // ���� ������ �迭
    public Transform spawnPoint; // ���� ���� ��ġ
    private GameObject currentBoss; // ���� ���� �ν��Ͻ�

    [Header("Spawn Settings")]
    public float spawnTime = 120f; // ���� ���� ��� �ð�
    private float timer;
    public bool isBossSpawned = false;

    [Header("Cameras")]
    public CinemachineVirtualCamera playerCamera; // �÷��̾� ī�޶�
    public CinemachineVirtualCamera bossCamera; // ���� ī�޶�

    [Header("Player Settings")]
    public Transform playerTransform; // �÷��̾� Transform
    public PlayerUI playerUI; // �÷��̾� UI ����

    [Header("Environment Settings")]
    public GameObject[] otherObjects; // ��Ż
    public GameObject Potal; // ��Ż

    [Header("UI Settings")]
    public BossUI bossUI; // ���� UI ����

    [Header("Effects")]
    public float bossIntroDuration = 3f; // ���� ���� ���� ���� �ð�

    private bool bossDefeated = false; // ���� óġ ���� Ȯ��

    private void Start()
    {
        timer = spawnTime; // �ʱ� Ÿ�̸� ����
        bossUI.gameObject.SetActive(false);
        Potal.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isBossSpawned && !bossDefeated) // ������ óġ���� �ʾ��� ���� Ÿ�̸� ����
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
            bossUI.gameObject.SetActive(true);
            int randomIndex = Random.Range(0, bossPrefabs.Length);
            GameObject selectedBossPrefab = bossPrefabs[randomIndex];

            // ���� ����
            currentBoss = Instantiate(selectedBossPrefab, spawnPoint.position, Quaternion.identity);
            isBossSpawned = true;

            ToggleObjects(false); // �� �� �ٸ� ������Ʈ ��Ȱ��ȭ

            // ������ EnemyHealth ������Ʈ�� ������
            EnemyHealth bossHealth = currentBoss.GetComponent<EnemyHealth>();
            if (bossHealth != null)
            {
                bossHealth.isBoss = true; // ���� ����
                bossHealth.OnHealthChanged += bossUI.UpdateBossHealth; // ü�� ���� �̺�Ʈ ����
                bossHealth.OnBossDeath += OnBossDefeated; // ���� ��� �̺�Ʈ ����

                // EnemyData Ȯ��
                if (bossHealth.enemyData != null)
                {
                    bossUI.SetBoss(currentBoss, bossHealth.enemyData.enemyName, bossHealth.enemyData.startHealth); // UI ����
                }


                // ī�޶� ����
                bossCamera.Follow = currentBoss.transform;
                bossCamera.LookAt = currentBoss.transform;
                bossCamera.Priority = 20;

                StartCoroutine(BossIntroSequence());
            }
        }
    }

    private IEnumerator BossIntroSequence()
    {
        // ���� ���� ���� -> �� ���� �ߴ�
        FindObjectOfType<EnemySpawner>().StartBossIntro();

        // 1. ���� �������� ī�޶� �̵�
        yield return StartCoroutine(FocusOnBossFront());

        // 2. �� �� ȿ��
        yield return StartCoroutine(ZoomInEffect());

        // 3. ���ο� ��� ȿ�� ����
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(1f);

        // 4. ī�޶� ��鸲 ȿ��
        EnableCameraShake(true);
        yield return new WaitForSeconds(1f);
        EnableCameraShake(false);

        // 5. ���ο� ��� ����
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        // 6. �÷��̾� ī�޶�� ����
        bossCamera.Priority = 0;
        playerCamera.Priority = 10;

        // 7. ���� ���� ���� -> �� ���� �簳
        FindObjectOfType<EnemySpawner>().EndBossIntro();

        // 8. �ٸ� ������Ʈ �ٽ� Ȱ��ȭ
        ToggleObjects(true); // ������Ʈ ����
    }

    public void OnBossDefeated()
    {
        if (currentBoss != null)
        {
            // ���� ����
            Destroy(currentBoss);
            currentBoss = null;
            isBossSpawned = false;
            bossDefeated = true; // ���� óġ ���·� ����

            // ���� UI ��Ȱ��ȭ
            bossUI.gameObject.SetActive(false);

            // �÷��̾� UI�� �޽��� ǥ��
            if (playerUI != null)
            {
                playerUI.UpdateMessage("Go Next Level");
                Potal.gameObject.SetActive(true);
            }

            // �� ���� ����
            EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
            if (spawner != null)
            {
                spawner.StopAllCoroutines();
            }
        }
    }

    private void ToggleObjects(bool isActive)
    {
        // ������ �÷��̾ ������ ������Ʈ Ȱ��ȭ/��Ȱ��ȭ
        foreach (GameObject obj in otherObjects)
        {
            if (obj != null && obj.name != "Map") // Environment ��ü�� ����
            {
                obj.SetActive(isActive);
            }
        }
    }


    private void DisableBossNavMeshAgent()
    {
        // NavMeshAgent ��Ȱ��ȭ
        if (currentBoss.TryGetComponent<UnityEngine.AI.NavMeshAgent>(out UnityEngine.AI.NavMeshAgent agent))
        {
            agent.enabled = false;
        }
    }

    private void EnableBossNavMeshAgent()
    {
        // NavMeshAgent Ȱ��ȭ
        if (currentBoss.TryGetComponent<UnityEngine.AI.NavMeshAgent>(out UnityEngine.AI.NavMeshAgent agent))
        {
            agent.enabled = true;
        }
    }

    private IEnumerator FocusOnBossFront()
    {
        var transposer = bossCamera.GetCinemachineComponent<CinemachineTransposer>();
        if (transposer == null || currentBoss == null) yield break;

        // ���� ���� ��ġ ���
        Vector3 bossPosition = currentBoss.transform.position;
        Vector3 bossForward = currentBoss.transform.forward; // ������ ���� ����
        Vector3 cameraPosition = bossPosition - bossForward * -10f + Vector3.up * 5f; // ���鿡�� �ణ ����

        // ī�޶� ��ġ ����
        Vector3 startOffset = transposer.m_FollowOffset;
        Vector3 targetOffset = cameraPosition - bossPosition;

        float duration = 1.5f; // �̵� �ð�
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transposer.m_FollowOffset = Vector3.Lerp(startOffset, targetOffset, elapsed / duration);
            yield return null;
        }
    }

    private IEnumerator ZoomInEffect()
    {
        float startFOV = bossCamera.m_Lens.FieldOfView;
        float targetFOV = 30f; // Ȯ�� ȿ��
        float duration = 1.5f; // �� �� �ð�

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            bossCamera.m_Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, elapsed / duration);
            yield return null;
        }
    }

    private void EnableCameraShake(bool enable)
    {
        var noise = bossCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (noise != null)
        {
            noise.m_AmplitudeGain = enable ? 2.0f : 0f; // ��鸲 ����
            noise.m_FrequencyGain = enable ? 2.0f : 0f; // ��鸲 ��
        }
    }

}
