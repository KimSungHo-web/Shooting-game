using UnityEngine;

public class DestroyParent : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(transform.parent.gameObject);
        }
    }

}