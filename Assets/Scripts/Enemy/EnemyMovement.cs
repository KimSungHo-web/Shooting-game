using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //�÷��̾ �߷��ؾ��Ѵ� -> �÷��̾� ��ġ�� ü���� �������?���� 
    [SerializeField] private Transform playerTransform;
    [SerializeField] private ThirdPersonMovement playerMovement;
    [SerializeField] private PlayerHealthSystem healthSystem;
    [SerializeField] private EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent agent;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        playerMovement = player.GetComponent<ThirdPersonMovement>();
        healthSystem = player.GetComponent<PlayerHealthSystem>();
        enemyHealth = GetComponent<EnemyHealth>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (agent != null && agent.enabled && enemyHealth?.currentHealth > 0 && healthSystem?.health > 0)
        {
            Vector3 targetPosition = GameManager.Instance.playerMovement.CurrentPosition;
            agent.SetDestination(targetPosition);
        }
        else
        {
            // NavMeshAgent ��Ȱ��ȭ
            if (agent != null && agent.enabled)
            {
                agent.enabled = false;
            }
        }
    }
}
