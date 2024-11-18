using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("AttackState")]
    private EnemyData enemyData;
    public float attackRate = 0.5f; //공격 (쿨타임)
    bool playerInRange; // 플레이어가 사거리 내에 있는지 여부
    float attacktimer; // 공격 시간 측정용 타이머

    [Header("Components")]
    Animator anim;
    GameObject player;
    //플레이어 체력
    EnemyHealth enemyHealth;



    void Awake()
    {
        EnemyDataManager enemydatamanager =GetComponent<EnemyDataManager>();
        enemyData = enemydatamanager.enemyData;
        player = GameObject.FindGameObjectWithTag("Player");
        //플레이어 체력
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true; //콜라이더가 겹쳐지면 플레이어가 적의 공격 범위 안에 들어온것으로 판단
            enemyHealth.currentHealth -= enemyData.attackDamage;
        }
    }

    void Update()
    {
        attacktimer += Time.deltaTime; // 공격 시간 누적
        //// 공격 쿨타임이 지나고, 플레이어가 사거리 안에 있으며, 적이 살아 있을 때 공격
        if (attacktimer >= attackRate && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        //플레이어 체력이 0이하면
        anim.SetTrigger("PlayerDead");
    }
    void Attack()
    {
        attacktimer = 0f; //공격을 하게되면 초기화를 시켜다시 시간을 잼

        //플레이어 현재 체력이 0보다 높으면 발동
        //플레이어가 피해를 받는 다 공격데미지만큼
    }
}
