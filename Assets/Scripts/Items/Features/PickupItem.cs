using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //�κ��丮�� ���� �ż��� ���⿡

        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

    }
}
