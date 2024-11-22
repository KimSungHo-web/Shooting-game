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
            playerInRange = true; //콜라이더가 겹쳐지면 플레이어가 적의 공격 범위 안에 들어온것으로 판단
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false; // 콜라이더에서 나가면 플레이어가 사거리 밖으로 나갔음을 판단
        }
    }
    void Update()
    {
        attacktimer += Time.deltaTime; // 공격 시간 누적

        // 공격 쿨타임이 지나고, 플레이어가 사거리 안에 있으며, 적이 살아 있을 때 공격
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
        //플레이어 현재 체력이 0보다 높으면 발동
        if (healthSystem.health > 0) 
        {
            attacktimer = 0f; // 공격 쿨타임 초기화
            bool damageApplied = healthSystem.ChangeHealth(-enemyData.attackDamage);
            Debug.Log($"플레이어가 {enemyData.attackDamage} 데미지를 입었습니다. 현재 체력: {healthSystem.health}");
        }
    }
}
