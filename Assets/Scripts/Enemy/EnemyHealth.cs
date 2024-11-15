using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startHealth = 100; //�ʱ� ü��
    public int currentHealth; //����ü��
    public float disappearSpeed = 2.5f; //������� �ð�
    public int goldValue = 10; //���
    public int expValue = 10; //����ġ
    public AudioClip deathClip; //�״� �Ҹ�

    Animator animator; //�ִϸ�����
    AudioSource enemyAudio; // �� �����
    ParticleSystem hitParticles; // ��Ʈ ��ƼŬ
    CapsuleCollider capsuleCollider; 
    bool isDead; //��������
    bool isdisappearing; // ������� ��

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

        //���߰� ����ġ �߰�
        //�����Ŵ���.�ν��ٽ�?.��� += goldValue
        //�����Ŵ���.�ν��ٽ�?.����ġ += expValue

        Destroy(gameObject, 2f);
    }
}
