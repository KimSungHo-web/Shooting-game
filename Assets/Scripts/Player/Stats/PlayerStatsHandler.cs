using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;

    public PlayerStats playerStats => _playerStats;
}
