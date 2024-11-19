using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ThridPersonMovement : MonoBehaviour
{
    private PlayerController _playerController;
    private CharacterController _characterController;
    
    private GameObject _mainCamera;
    private PlayerStats _playerStats;

    private Vector2 _inputDirection;

    public Vector3 CurrentPosition => transform.position; // 플레이어의 현재 위치 반환(KSH추가)
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _characterController = GetComponent<CharacterController>();

        _mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
        _playerStats = GetComponent<PlayerStatsHandler>().playerStats;
    }
    private void Start()
    {
        GameManager.Instance.playerMovement = this;
    }
    private void OnEnable()
    {
        _playerController.MoveEvent += PlayerController_MoveEvent;
    }

    private void OnDisable()
    {
        _playerController.MoveEvent -= PlayerController_MoveEvent;
    }

    private void Update()
    {
        Move();
    }

    private void PlayerController_MoveEvent (Vector2 direction)
    {
        _inputDirection = direction;
    }

    private void Move()
    {
        if (_inputDirection != Vector2.zero)
        {
            float moveSpeedScaler = _playerController.isAttacking ? 0.5f : 1f;
            float speed = _playerStats.moveSpeed * moveSpeedScaler;

            float targetAngle = Mathf.Atan2 (_inputDirection.x, _inputDirection.y) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
            Vector3 moveDirection = Quaternion.Euler (0f, targetAngle, 0f) * Vector3.forward;
            
            _characterController.Move (speed * Time.deltaTime * moveDirection);
        }
    }

}
