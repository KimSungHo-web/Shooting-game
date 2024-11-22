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
    PlayerHealthSystem healthSystem;
    EnemyHealth enemyHealth;



    void Awake()
    {
        EnemyDataManager enemydatamanager =GetComponent<EnemyDataManager>();
        enemyData = enemydatamanager.enemyData;

        player = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        healthSystem = player.GetComponentInChildren<PlayerHealthSystem>();

        enemyHealth = GetComponentInChildren<EnemyHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true; //�ݶ��̴��� �������� �÷��̾ ���� ���� ���� �ȿ� ���°����� �Ǵ�
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false; // �ݶ��̴����� ������ �÷��̾ ��Ÿ� ������ �������� �Ǵ�
        }
    }
    void Update()
    {
        attacktimer += Time.deltaTime; // ���� �ð� ����

        // ���� ��Ÿ���� ������, �÷��̾ ��Ÿ� �ȿ� ������, ���� ��� ���� �� ����
        if (attacktimer >= attackRate && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        if (healthSystem.health <= 0)
        {
            anim.SetTrigger("PlayerDead");
            return;
        }
    }
    void Attack()
    {
        //�÷��̾� ���� ü���� 0���� ������ �ߵ�
        if (healthSystem.health > 0) 
        {
            attacktimer = 0f; // ���� ��Ÿ�� �ʱ�ȭ
            bool damageApplied = healthSystem.ChangeHealth(-enemyData.attackDamage);
            Debug.Log($"�÷��̾ {enemyData.attackDamage} �������� �Ծ����ϴ�. ���� ü��: {healthSystem.health}");
        }
    }
}
