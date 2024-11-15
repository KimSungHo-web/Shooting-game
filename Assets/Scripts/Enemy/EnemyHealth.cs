using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startHealth = 100; //초기 체력
    public int currentHealth; //현재체력
    public float disappearSpeed = 2.5f; //사라지는 시간
    public int goldValue = 10; //골드
    public int expValue = 10; //경험치
    public AudioClip deathClip; //죽는 소리

    Animator animator; //애니메이터
    AudioSource enemyAudio; // 적 오디오
    ParticleSystem hitParticles; // 히트 파티클
    CapsuleCollider capsuleCollider; 
    bool isDead; //죽은상태
    bool isdisappearing; // 사라지는 중

    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startHealth;
    }

    void Update()
    {
        if (isdisappearing)
        {
            
            transform.Translate(-Vector3.up * disappearSpeed * Time.deltaTime);
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
    }
    public void Disappearing() 
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        GetComponent<Rigidbody>().isKinematic = true;

        isdisappearing = true;

        //돈추가 경험치 추가
        //참조매니저.인스텐스?.골드 += goldValue
        //참조매니저.인스텐스?.경험치 += expValue

        Destroy(gameObject, 2f);
    }
}
