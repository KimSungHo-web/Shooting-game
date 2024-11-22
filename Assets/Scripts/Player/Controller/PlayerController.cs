using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector2> MoveEvent = (normalizedMoveDirection) => { };
    public event Action<Vector2> LookEvent = (mouseDelta) => { };
    public event Action FireEvent = () => { };
    public event Action<bool> SprintKeyPressed = (pressed) => { };

    public bool isAttacking { get; protected set; }
    public bool isPressingSprintKey { get; protected set; }
    
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

    public void CallSprintKeyPressed (bool sprint)
    {
        SprintKeyPressed.Invoke (sprint);
    }
}
