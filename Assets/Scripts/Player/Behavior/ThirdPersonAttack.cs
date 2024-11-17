using UnityEngine;
using UnityEngine.Serialization;

public class ThirdPersonAttack : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float _aimAlignmentThreshold = 0.9f;
        
    private PlayerController _playerController;

    private Camera _mainCamera;
    private PlayerStats _playerStats;

    private float _aimAlignment;
    private float _attackDelayCounter = 0;

    
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerStats = GetComponent<PlayerStatsHandler>().playerStats;

        _mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        if (_attackDelayCounter < _playerStats.attackDelay)
        {
            _attackDelayCounter += Time.deltaTime;
        }
        else
        {
            if (_playerController.isAttacking)
            {
                Vector3 playerHorizontalForward = transform.forward;
                playerHorizontalForward.y = 0f;
                playerHorizontalForward.Normalize();
                
                Vector3 cameraHorizontalForward = _mainCamera.transform.forward;
                cameraHorizontalForward.y = 0f;
                cameraHorizontalForward.Normalize();

                _aimAlignment = Vector3.Dot (playerHorizontalForward, cameraHorizontalForward);

                if (_aimAlignment >= _aimAlignmentThreshold)
                {
                    _playerController.CallFireEvent();
                    _attackDelayCounter = 0f;
                }
            }
        }
    }

}
