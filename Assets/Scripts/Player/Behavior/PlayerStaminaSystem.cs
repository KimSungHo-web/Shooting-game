using System;
using UnityEngine;

public class PlayerStaminaSystem : MonoBehaviour
{
    public Action<int, int> StaminaChanged = (current, max) => { };
    
    
    [SerializeField]
    private int _stamina;

    [SerializeField]
    private float _recoveryDelay = 0.2f;

    [SerializeField]
    private int _recoveryRate = 1;

    [SerializeField]
    private float _useThresholdRate = 0.1f;
    
    
    public int stamina => _stamina;
    public int maxStamina => _playerStats.maxStamina;
    public float useThresholdRate => _useThresholdRate;
    
    
    private PlayerController _playerController;
    private PlayerStats _playerStats;
    private PlayerUI _playerUI;
    
    private float _recoveryDelayCounter;
    private int _using;

    
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerStats = GetComponent<PlayerStatsHandler>().playerStats;

        _stamina = maxStamina;
    }

    private void Start()
    {
        _playerUI = GameManager.Instance.playerUI;
    }

    private void Update()
    {
        if (_recoveryDelayCounter < _recoveryDelay)
            _recoveryDelayCounter += Time.deltaTime;
        
        RefreshStamina();
    }

    private void RefreshStamina()
    {
        if (_using != 0)
            return;
        if (_stamina == maxStamina)
            return;
        if (_recoveryDelayCounter < _recoveryDelay)
            return;
        
        _recoveryDelayCounter = 0f;

        _stamina = Mathf.Clamp (_stamina + _recoveryRate, 0, maxStamina);
        StaminaChanged.Invoke (_stamina, maxStamina);
        _playerUI.UpdateStamina (_stamina, maxStamina);
    }

    public void SetUsingState (bool bUsing)
    {
        _using += bUsing ? 1 : -1;
    }

    public bool Use (int amount)
    {
        if (_stamina - amount < 0f)
            return false;
        _stamina -= amount;
        StaminaChanged.Invoke (_stamina, maxStamina);
        _playerUI.UpdateStamina (_stamina, maxStamina);
        return true;
    }

    public bool CanUse()
    {
        float rate = (float)_stamina / maxStamina;
        if (rate < _useThresholdRate)
            return false;
        return true;
    }
}
