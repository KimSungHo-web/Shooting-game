using System;
using UnityEngine;

public class PlayerSprintController : MonoBehaviour
{
    [SerializeField]
    private int staminaUsage = 3;

    [SerializeField]
    private float staminaDrainDealy = 0.2f;
    
    
    private PlayerController _playerController;
    private PlayerStaminaSystem _staminaSystem;
    
    private bool _isSprinting = false;
    private float _staminaDrainDelayCounter;
    
    
    public bool isSprinting => _isSprinting;
    protected bool isPressingSprintKey => _playerController.isPressingSprintKey;

    
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerController.SprintKeyPressed += PlayerController_SprintKeyPressed;
        _staminaSystem = GetComponent<PlayerStaminaSystem>();
    }

    private void Update()
    {
        if (_staminaDrainDelayCounter < staminaDrainDealy)
            _staminaDrainDelayCounter += Time.deltaTime;
        
        DrainStamina();
    }

    private void DrainStamina()
    {
        if (!_isSprinting)
            return;
        if (_staminaDrainDelayCounter < staminaDrainDealy)
            return;

        _staminaDrainDelayCounter = 0f;
        
        bool used = _staminaSystem.Use (staminaUsage);
        if (!used)
        {
            _isSprinting = false;
            _staminaSystem.SetUsingState (false);
        }
    }
    
    private void PlayerController_SprintKeyPressed (bool pressed)
    {
        if (pressed)
        {
            if (_staminaSystem.CanUse())
            {
                _isSprinting = true;
                _staminaSystem.SetUsingState (true);
            }
        }
        else
        {
            if (_isSprinting)
            {
                _isSprinting = false;
                _staminaSystem.SetUsingState (false);
            }
        }
    }
}
