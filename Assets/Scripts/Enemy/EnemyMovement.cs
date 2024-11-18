using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //�÷��̾ �߷��ؾ��Ѵ� -> �÷��̾� ��ġ�� ü���� ������� ���� 
    Transform player;
    //�����ؾߵĵ�? �÷��̾� ü��
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent agent;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //�����ؾߵĵ�? �÷��̾� ü��
        enemyHealth = GetComponent<EnemyHealth>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (enemyHealth.currentHealth > 0/* && �÷��̾ ���������*/) 
        {
            //��θ� ���Ӱ� ����(�÷��̾��� ��ġ)
            agent.SetDestination(player.position);
        }
        else 
        {
            //�ƴ� ����
            agent.enabled = false;
        }
    }
}
