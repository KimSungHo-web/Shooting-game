using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour
{
    private const int _maxHealth = int.MaxValue;
    private const float _maxMoveSpeed = 30f;
    private const float _minAttackDelay = 0.01f;
    private const float _maxAttackDamage = int.MaxValue;
    
    
    [SerializeField] private PlayerStats _playerStats;

    public PlayerStats playerStats => _playerStats;
    
    
}
