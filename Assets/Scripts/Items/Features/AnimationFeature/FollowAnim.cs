using UnityEditor;
using UnityEngine;

public class FollowAnim : MonoBehaviour
{
    
    public float MinMoidifier = -3f;
    public float MaxMoidifier = -1f;

    public Transform Loot;
    public Transform Target;

    Vector3 _velocity = Vector3.zero;
    bool _isFollowing = false;
    private void Start()
    {
        Loot = transform.parent;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Target = player.transform;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isFollowing = true;
        }
       
    }
    public void StartFollowing()
    {
        _isFollowing = true;
    }

    private void Update()
    {
        if(_isFollowing)
        {
         Loot.position = Vector3.SmoothDamp
         (Loot.position, Target.position, ref _velocity,Time.deltaTime * Random.Range(MinMoidifier,MaxMoidifier));
        }
        
    }

}
