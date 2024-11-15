using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("AttackState")]
    public float attackRate = 0.5f; //���� (��Ÿ��)
    public int attackDamage = 10; //���� ������

    [Header("Components")]
    Animator anim;
    GameObject player;
    //�÷��̾� ü��
    EnemyHealth enemyHealth;
    bool playerInRange; // �÷��̾ ��Ÿ� ���� �ִ��� ����
    float attacktimer; // ���� �ð� ������ Ÿ�̸�


    void Awake()
    {
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
