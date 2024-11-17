using UnityEngine;
using UnityEngine.InputSystem;

public class ThridPersonInputController : ThirdPersonController
{
    public void OnMove(InputValue value)
    {
        Vector2 moveDirection = value.Get<Vector2>().normalized;
        CallMoveEvent (moveDirection);
    }

    public void OnLook (InputValue value)
    {
        Vector2 delta = value.Get<Vector2>();
        CallLookEvent (delta);
    }
}
