using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemyData enemyData;
    private EnemySpawner spawner;
    public int currentHealth;
    bool isDead;
    bool isDisappearing;
    Animator animator;
    AudioSource enemyAudio;

    ParticleSystem hitParticles; // 히트 파티클
    CapsuleCollider capsuleCollider;
    DropItem dropitem;

    

    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        spawner = FindObjectOfType<EnemySpawner>();
        EnemyDataManager enemydatamanager = GetComponent<EnemyDataManager>();
        enemyData = enemydatamanager.enemyData;

        currentHealth = enemyData.startHealth;

        dropitem = GetComponent<DropItem>();
        
    }

    void Update()
    {

        //테스트를 위해 추가한 코드입니다
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(20, transform.position); // T 키를 누르면 20 데미지
        }
        //테스트를 위해 추가한 코드입니다

        if (isDisappearing)
        {
            transform.Translate(-Vector3.up *enemyData.disappearSpeed * Time.deltaTime);
        }

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
    public void StartSinking() //Dead 애니메이션 이벤트가 저거로 설정되어잇는데 FBX파일 자체에 저장되어잇는거라 제거를 못함
    {
        dropitem.Drop();

        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        isDisappearing = true;

        GameManager.Instance.OnEnemyDeath(enemyData);
        spawner.RemoveEnemy(gameObject);
        Destroy(gameObject, 2f);
    }
}
