using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Dependencies")]
    public EnemyData enemyData; // �� ������
    [SerializeField] private EnemySpawner spawner; // �� ������

    [Header("Health Settings")]
    public int currentHealth; // ���� ü��
    private bool isDead;
    private bool isDisappearing;

    [Header("Boss Settings")]
    public bool isBoss = false; // ���� ����

    [Header("Components")]
    private Animator animator;
    private AudioSource enemyAudio;
    private ParticleSystem hitParticles;
    private CapsuleCollider capsuleCollider;
    private DropItem dropitem;

    // �̺�Ʈ
    public delegate void HealthChanged(int currentHealth, int maxHealth); // ü�� ���� �̺�Ʈ
    public event HealthChanged OnHealthChanged;

    public delegate void BossDeath(); // ���� ��� �̺�Ʈ
    public event BossDeath OnBossDeath;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        spawner = FindObjectOfType<EnemySpawner>();

        // ������ �ʱ�ȭ
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

        // ���� UI ������Ʈ
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
            OnBossDeath?.Invoke(); // ���� ��� �̺�Ʈ
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
