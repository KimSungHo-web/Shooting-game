using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemyData enemyData;
    public int currentHealth;
    bool isDead;
    bool isDisappearing;
    Animator animator;
    AudioSource enemyAudio;

    ParticleSystem hitParticles; // ��Ʈ ��ƼŬ
    CapsuleCollider capsuleCollider;
    private Testgold testgold;
    DropItem dropitem;

    

    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        testgold = FindObjectOfType<Testgold>();

        EnemyDataManager enemydatamanager = GetComponent<EnemyDataManager>();
        enemyData = enemydatamanager.enemyData;

        currentHealth = enemyData.startHealth;

        dropitem = GetComponent<DropItem>();
        
    }

    void Update()
    {

        //�׽�Ʈ�� ���� �߰��� �ڵ��Դϴ�
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(20, transform.position); // T Ű�� ������ 20 ������
        }

        if (isDisappearing)
        {
            transform.Translate(-Vector3.up *enemyData.disappearSpeed * Time.deltaTime);
        }
        //�׽�Ʈ�� ���� �߰��� �ڵ��Դϴ�
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead) 
        {
            return;
        }

        enemyAudio.Play();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;

        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        isDead = true;
        animator.SetTrigger("Dead");
    }
    public void StartSinking() //Dead �ִϸ��̼� �̺�Ʈ�� ���ŷ� �����Ǿ��մµ� FBX���� ��ü�� ����Ǿ��մ°Ŷ� ���Ÿ� ����
    {
        dropitem.Drop();

        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        isDisappearing = true;

        testgold.AddGold(enemyData.goldValue);
        testgold.AddEXP(enemyData.expValue);
        Destroy(gameObject, 2f);
    }
}
