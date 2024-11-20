using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //플레이어를 추력해야한다 -> 플레이어 위치와 체력을 기반으로 추적 
    [SerializeField] private Transform playerTransform;
    [SerializeField] private ThridPersonMovement playerMovement;
    [SerializeField] private PlayerHealthSystem healthSystem;
    [SerializeField] private EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent agent;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        playerMovement = player.GetComponent<ThridPersonMovement>();
        healthSystem = player.GetComponent<PlayerHealthSystem>();
        enemyHealth = GetComponent<EnemyHealth>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (agent != null && agent.enabled && enemyHealth?.currentHealth > 0 && healthSystem?.health > 0)
        {
            // 경로를 새롭게 설정 (플레이어의 위치)
            Vector3 targetPosition = GameManager.Instance?.playerMovement?.CurrentPosition ?? Vector3.zero;
            agent.SetDestination(targetPosition);
        }
        else
        {
            // NavMeshAgent 비활성화
            if (agent != null && agent.enabled)
            {
                agent.enabled = false;
            }
        }
    }
}
