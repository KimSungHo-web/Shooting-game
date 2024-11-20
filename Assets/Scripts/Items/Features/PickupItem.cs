using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //인벤토리로 가는 매서드 여기에

        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

    }
}
