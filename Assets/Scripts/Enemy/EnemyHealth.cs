using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Dependencies")]
    public EnemyData enemyData; // 적 데이터
    [SerializeField] private EnemySpawner spawner; // 적 스포너

    [Header("Health Settings")]
    public int currentHealth; // 현재 체력
    private bool isDead;
    private bool isDisappearing;

    [Header("Boss Settings")]
    public bool isBoss = false; // 보스 여부

    [Header("Components")]
    private Animator animator;
    private AudioSource enemyAudio;
    private ParticleSystem hitParticles;
    private CapsuleCollider capsuleCollider;
    private DropItem dropitem;

    // 이벤트
    public delegate void HealthChanged(int currentHealth, int maxHealth); // 체력 변경 이벤트
    public event HealthChanged OnHealthChanged;

    public delegate void BossDeath(); // 보스 사망 이벤트
    public event BossDeath OnBossDeath;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        spawner = FindObjectOfType<EnemySpawner>();

        // 데이터 초기화
        EnemyDataManager enemyDataManager = GetComponent<EnemyDataManager>();
        enemyData = enemyDataManager.enemyData;

        currentHealth = enemyData.startHealth;
        dropitem = GetComponent<DropItem>();
    }

    private void Update()
    {
        if (isDisappearing)
        {
            transform.Translate(-Vector3.up * enemyData.disappearSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        enemyAudio.Play();
        currentHealth -= amount;

        // 보스 UI 업데이트
        if (isBoss)
        {
            OnHealthChanged?.Invoke(currentHealth, enemyData.startHealth);
        }

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        animator.SetTrigger("Dead");

        if (isBoss)
        {
            OnBossDeath?.Invoke(); // 보스 사망 이벤트
        }
    }

    public void StartSinking()
    {
        GameManager.Instance.OnEnemyDeath(enemyData, gameObject);

        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        isDisappearing = true;
        spawner.RemoveEnemy(gameObject);

        Destroy(gameObject, 2f);
    }
}
