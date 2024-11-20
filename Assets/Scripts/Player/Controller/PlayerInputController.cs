using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : PlayerController
{
    [HideInInspector]
    public bool canLook = true;
    public Action inventory;

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

    public void OnAttack (InputValue value)
    {
        isAttacking = value.isPressed;
    }

    public void OnSprint (InputValue value)
    {
        isPressingSprintKey = value.isPressed;
        CallSprintKeyPressed (isPressingSprintKey);
    }

    public void OnInventory()
    {
        inventory?.Invoke();
        ToggleCursor();
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
