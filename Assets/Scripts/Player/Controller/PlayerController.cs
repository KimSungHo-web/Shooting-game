using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector2> MoveEvent = (_) => { };
    public event Action<Vector2> LookEvent = (_) => { };
    public event Action FireEvent = () => { };

    public bool isAttacking { get; protected set; }
    
    public void CallMoveEvent (Vector2 direction)
    {
        MoveEvent.Invoke (direction);
    }

    public void CallLookEvent (Vector2 position)
    {
        LookEvent.Invoke (position);
    }

    public void CallFireEvent ()
    {
        FireEvent.Invoke ();
    }
}
