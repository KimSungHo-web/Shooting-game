using System;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _healthChangeDelay = 0.5f;

    public int health => _health;
    public int maxHealth => _playerStats.maxHealth;

    public event Action DieEvent = () => { };
    public event Action<int> HealthChangedEvent = (_) => { };

    private PlayerStats _playerStats;
    private float _healthChangeDelayCounter;

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStatsHandler>().playerStats;
        
        _health = maxHealth;
    }

    private void Update()
    {
        if (_healthChangeDelayCounter < _healthChangeDelay)
        {
            _healthChangeDelayCounter += Time.deltaTime;
        }
    }

    public bool ChangeHealth (int amount)
    {
        if (_healthChangeDelayCounter >= _healthChangeDelay)
        {
            _healthChangeDelayCounter = 0f;
            
            _health = Mathf.Clamp (_health + amount, 0, _playerStats.maxHealth);
            HealthChangedEvent.Invoke (_health);

            if (_health == 0)
            {
                DieEvent.Invoke();
            }

            return true;
        }

        return false;
    }
    
}
