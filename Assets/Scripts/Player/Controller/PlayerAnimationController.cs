using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationController : MonoBehaviour
{
    private static readonly int _animID_IsWalking = Animator.StringToHash ("IsWalking");
    private static readonly int _animID_Die = Animator.StringToHash ("Die");
    
    private PlayerController _playerController;
    private Animator _animator;
    private bool _isWalking = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _playerController.MoveEvent += PlayerController_MoveEvent;
    }

    private void PlayerController_MoveEvent (Vector2 vector)
    {
        if (vector == Vector2.zero) _isWalking = false;
        else _isWalking = true;
    }

    private void Update()
    {
        if (_playerController.isAttacking)
        {
            _animator.SetBool (_animID_IsWalking, false);
        }
        else if (_isWalking)
        {
            _animator.SetBool (_animID_IsWalking, true);
        }
        else
        {
            _animator.SetBool (_animID_IsWalking, false);
        }
    }
}
