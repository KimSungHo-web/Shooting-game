using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("AttackState")]
    private EnemyData enemyData;
    public float attackRate = 0.5f; //���� (��Ÿ��)
    bool playerInRange; // �÷��̾ ��Ÿ� ���� �ִ��� ����
    float attacktimer; // ���� �ð� ������ Ÿ�̸�

    [Header("Components")]
    Animator anim;
    GameObject player;
    //�÷��̾� ü��
    EnemyHealth enemyHealth;



    void Awake()
    {
        EnemyDataManager enemydatamanager =GetComponent<EnemyDataManager>();
        enemyData = enemydatamanager.enemyData;
        player = GameObject.FindGameObjectWithTag("Player");
        //�÷��̾� ü��
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true; //�ݶ��̴��� �������� �÷��̾ ���� ���� ���� �ȿ� ���°����� �Ǵ�
            enemyHealth.currentHealth -= enemyData.attackDamage;
        }
    }

    void Update()
    {
        attacktimer += Time.deltaTime; // ���� �ð� ����
        //// ���� ��Ÿ���� ������, �÷��̾ ��Ÿ� �ȿ� ������, ���� ��� ���� �� ����
        if (attacktimer >= attackRate && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        //�÷��̾� ü���� 0���ϸ�
        anim.SetTrigger("PlayerDead");
    }
    void Attack()
    {
        attacktimer = 0f; //������ �ϰԵǸ� �ʱ�ȭ�� ���Ѵٽ� �ð��� ��

        //�÷��̾� ���� ü���� 0���� ������ �ߵ�
        //�÷��̾ ���ظ� �޴� �� ���ݵ�������ŭ
    }
}
