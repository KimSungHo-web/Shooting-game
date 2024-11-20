using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSprintController : MonoBehaviour
{
    [SerializeField]
    private int staminaUsage = 3;

    [SerializeField]
    private float staminaDrainDealy = 0.2f;
    
    
    private PlayerController _playerController;
    private PlayerStaminaSystem _staminaSystem;
    private ThirdPersonMovement _movement;
    
    private bool _sprinting = false;
    private bool _pressingSprintKey;
    private float _staminaDrainDelayCounter;
    private bool _prevMoving;
    
    
    public bool isSprinting => _sprinting;
    private bool _moving => _movement.currentSpeed >= 0.1f;

    
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _staminaSystem = GetComponent<PlayerStaminaSystem>();
        _movement = GetComponent<ThirdPersonMovement>();
    }

    private void Update()
    {
        if (_staminaDrainDelayCounter < staminaDrainDealy)
            _staminaDrainDelayCounter += Time.deltaTime;
        
        UpdateState();
        DrainStamina();
    }

    private void DrainStamina()
    {
        if (!_sprinting)
            return;
        if (_staminaDrainDelayCounter < staminaDrainDealy)
            return;

        _staminaDrainDelayCounter = 0f;
        
        bool used = _staminaSystem.Use (staminaUsage);
        if (!used)
            ChangeState (false);
    }

    private void OnSprint (InputValue value)
    {
        _pressingSprintKey = value.isPressed;
    }

    private void UpdateState()
    {
        if (_pressingSprintKey)
        {
            if (_moving)
            {
                if (!_sprinting)
                {
                    if (_staminaSystem.CanUse())
                        ChangeState (true);    
                }
                
            }
            else
                ChangeState (false);
            
        }
        else
            ChangeState (false);
        
        /*
        // Is the sprint key being pressed?
        if (_pressingSprintKey)
        {
            // Has movement newly started?
            if (_prevMoving != _moving)
            {
                _prevMoving = _moving;
                if (_moving)
                {
                    if (_staminaSystem.CanUse())
                        ChangeState (true);
                }
                else
                    ChangeState (false);
            }
            // Is currently moving?
            else
            {
                if (_moving && !_sprinting)
                {
                    if (_staminaSystem.CanUse())
                        ChangeState (true);
                }
            }
        }
        else
        {
            ChangeState (false);
            _prevMoving = false;
        }
        */
    }

    private void ChangeState (bool sprintState)
    {
        if (_sprinting == sprintState)
            return;

        _sprinting = sprintState;
        _staminaSystem.SetUsingState (sprintState);
    }
    
}
