using UnityEditor;
using UnityEngine;

public class Follow : MonoBehaviour
{
    
    public float MinMoidifier = 1f;
    public float MaxMoidifier = 3f;

    public Transform Loot;
    public Transform Target;

    Vector3 _velocity = Vector3.zero;
    bool _isFollowing = false;

    public void StartFollowing()
    {
        _isFollowing = true;
    }

    private void Update()
    {
        if(_isFollowing)
        {
         Loot.position = Vector3.SmoothDamp
         (Loot.position, Target.position, ref _velocity,Random.Range(MinMoidifier,MaxMoidifier));
        }
        
    }

}
