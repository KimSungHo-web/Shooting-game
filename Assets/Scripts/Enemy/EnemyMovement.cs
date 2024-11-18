using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //플레이어를 추력해야한다 -> 플레이어 위치와 체력을 기반으로 추적 
    Transform player;
    //참조해야돨듯? 플레이어 체력
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent agent;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //참조해야돨듯? 플레이어 체력
        enemyHealth = GetComponent<EnemyHealth>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (enemyHealth.currentHealth > 0/* && 플레이어가 살아있으면*/) 
        {
            //경로를 새롭게 쓴다(플레이어의 위치)
            agent.SetDestination(player.position);
        }
        else 
        {
            //아님 말구
            agent.enabled = false;
        }
    }
}
