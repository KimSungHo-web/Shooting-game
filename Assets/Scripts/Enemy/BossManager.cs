using System.Collections;
using UnityEngine;
using Cinemachine;

public class BossManager : MonoBehaviour
{
    [Header("Boss Settings")]
    public GameObject[] bossPrefabs; // 보스 프리팹 배열
    public Transform spawnPoint; // 보스 스폰 위치
    private GameObject currentBoss; // 현재 보스 인스턴스

    [Header("Spawn Settings")]
    public float spawnTime = 120f; // 보스 생성 대기 시간
    private float timer;
    public bool isBossSpawned = false;

    [Header("Cameras")]
    public CinemachineVirtualCamera playerCamera; // 플레이어 카메라
    public CinemachineVirtualCamera bossCamera; // 보스 카메라

    [Header("Player Settings")]
    public Transform playerTransform; // 플레이어 Transform
    public PlayerUI playerUI; // 플레이어 UI 연결

    [Header("Environment Settings")]
    public GameObject[] otherObjects; // 포탈
    public GameObject Potal; // 포탈

    [Header("UI Settings")]
    public BossUI bossUI; // 보스 UI 관리

    [Header("Effects")]
    public float bossIntroDuration = 3f; // 보스 등장 연출 지속 시간

    private bool bossDefeated = false; // 보스 처치 여부 확인

    private void Start()
    {
        timer = spawnTime; // 초기 타이머 설정
        bossUI.gameObject.SetActive(false);
        Potal.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isBossSpawned && !bossDefeated) // 보스가 처치되지 않았을 때만 타이머 동작
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

            // 보스 생성
            currentBoss = Instantiate(selectedBossPrefab, spawnPoint.position, Quaternion.identity);
            isBossSpawned = true;

            ToggleObjects(false); // 맵 내 다른 오브젝트 비활성화

            // 보스의 EnemyHealth 컴포넌트를 가져옴
            EnemyHealth bossHealth = currentBoss.GetComponent<EnemyHealth>();
            if (bossHealth != null)
            {
                bossHealth.isBoss = true; // 보스 설정
                bossHealth.OnHealthChanged += bossUI.UpdateBossHealth; // 체력 변경 이벤트 연결
                bossHealth.OnBossDeath += OnBossDefeated; // 보스 사망 이벤트 연결

                // EnemyData 확인
                if (bossHealth.enemyData != null)
                {
                    bossUI.SetBoss(currentBoss, bossHealth.enemyData.enemyName, bossHealth.enemyData.startHealth); // UI 설정
                }


                // 카메라 설정
                bossCamera.Follow = currentBoss.transform;
                bossCamera.LookAt = currentBoss.transform;
                bossCamera.Priority = 20;

                StartCoroutine(BossIntroSequence());
            }
        }
    }

    private IEnumerator BossIntroSequence()
    {
        // 보스 연출 시작 -> 적 스폰 중단
        FindObjectOfType<EnemySpawner>().StartBossIntro();

        // 1. 보스 정면으로 카메라 이동
        yield return StartCoroutine(FocusOnBossFront());

        // 2. 줌 인 효과
        yield return StartCoroutine(ZoomInEffect());

        // 3. 슬로우 모션 효과 시작
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(1f);

        // 4. 카메라 흔들림 효과
        EnableCameraShake(true);
        yield return new WaitForSeconds(1f);
        EnableCameraShake(false);

        // 5. 슬로우 모션 종료
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        // 6. 플레이어 카메라로 복귀
        bossCamera.Priority = 0;
        playerCamera.Priority = 10;

        // 7. 보스 연출 종료 -> 적 스폰 재개
        FindObjectOfType<EnemySpawner>().EndBossIntro();

        // 8. 다른 오브젝트 다시 활성화
        ToggleObjects(true); // 오브젝트 복구
    }

    public void OnBossDefeated()
    {
        if (currentBoss != null)
        {
            // 보스 삭제
            Destroy(currentBoss);
            currentBoss = null;
            isBossSpawned = false;
            bossDefeated = true; // 보스 처치 상태로 설정

            // 보스 UI 비활성화
            bossUI.gameObject.SetActive(false);

            // 플레이어 UI에 메시지 표시
            if (playerUI != null)
            {
                playerUI.UpdateMessage("Go Next Level");
                Potal.gameObject.SetActive(true);
            }

            // 적 스폰 중지
            EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
            if (spawner != null)
            {
                spawner.StopAllCoroutines();
            }
        }
    }

    private void ToggleObjects(bool isActive)
    {
        // 보스와 플레이어를 제외한 오브젝트 활성화/비활성화
        foreach (GameObject obj in otherObjects)
        {
            if (obj != null && obj.name != "Map") // Environment 객체는 제외
            {
                obj.SetActive(isActive);
            }
        }
    }


    private void DisableBossNavMeshAgent()
    {
        // NavMeshAgent 비활성화
        if (currentBoss.TryGetComponent<UnityEngine.AI.NavMeshAgent>(out UnityEngine.AI.NavMeshAgent agent))
        {
            agent.enabled = false;
        }
    }

    private void EnableBossNavMeshAgent()
    {
        // NavMeshAgent 활성화
        if (currentBoss.TryGetComponent<UnityEngine.AI.NavMeshAgent>(out UnityEngine.AI.NavMeshAgent agent))
        {
            agent.enabled = true;
        }
    }

    private IEnumerator FocusOnBossFront()
    {
        var transposer = bossCamera.GetCinemachineComponent<CinemachineTransposer>();
        if (transposer == null || currentBoss == null) yield break;

        // 보스 정면 위치 계산
        Vector3 bossPosition = currentBoss.transform.position;
        Vector3 bossForward = currentBoss.transform.forward; // 보스의 정면 방향
        Vector3 cameraPosition = bossPosition - bossForward * -10f + Vector3.up * 5f; // 정면에서 약간 위쪽

        // 카메라 위치 조정
        Vector3 startOffset = transposer.m_FollowOffset;
        Vector3 targetOffset = cameraPosition - bossPosition;

        float duration = 1.5f; // 이동 시간
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
        float targetFOV = 30f; // 확대 효과
        float duration = 1.5f; // 줌 인 시간

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
            noise.m_AmplitudeGain = enable ? 2.0f : 0f; // 흔들림 강도
            noise.m_FrequencyGain = enable ? 2.0f : 0f; // 흔들림 빈도
        }
    }

}
