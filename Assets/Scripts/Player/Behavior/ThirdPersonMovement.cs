using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField]
    private float _speedChangeRate = 10f;
    
    [Tooltip ("The character uses its own gravity value. The engine default is -9.81f")]
    [SerializeField]
    private float _gravity = -9.81f;

    [Header ("Player Grounded")]

    [Tooltip ("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    [SerializeField]
    private bool _grounded = true;

    [SerializeField]
    private float _groundedOffset = -0.2f;
    
    [Tooltip ("What layers the character uses as ground")]
    [SerializeField]
    private LayerMask _groundLayers;
    
    
    private PlayerController _playerController;
    private CharacterController _characterController;
    private PlayerSprintController _sprintController;
    
    private GameObject _mainCamera;
    private PlayerStats _playerStats;

    private float _colliderRadius;
    private Vector3 _localSpherePosition;
    private Vector3 _spherePosition;
    private float _speed;

    // player
    private Vector2 _inputDirection;
    private float _verticalVelocity;
    private const float _terminalVelocity = 53f;
    private float _targetAngle;

    public float currentSpeed => _speed;
    public bool isMoving => _speed >= 0.1f;
    public Vector3 CurrentPosition => transform.position; // �÷��̾��� ���� ��ġ ��ȯ(KSH�߰�)
    
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _characterController = GetComponent<CharacterController>();
        _sprintController = GetComponent<PlayerSprintController>();

        _mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
        _playerStats = GetComponent<PlayerStatsHandler>().playerStats;

        _colliderRadius = _characterController.radius;
        float yOffset = _characterController.height / 2f - _characterController.radius - _groundedOffset; 
        _localSpherePosition = _characterController.center - new Vector3 (0, yOffset, 0f);
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
        JumpAndGravity();
        GroundedCehck();
        Move();
    }

    private void PlayerController_MoveEvent (Vector2 direction)
    {
        _inputDirection = direction;
    }

    private void Move()
    {
        float targetSpeed = 0f;
        
        if (_inputDirection != Vector2.zero)
        {
            targetSpeed = _playerStats.moveSpeed;
            targetSpeed *= _sprintController.isSprinting ? 1.5f : 1f;
            targetSpeed *= _playerController.isAttacking ? 0.5f : 1f;
        }

        float currentHorizontalSpeed = new Vector3 (_characterController.velocity.x, 0f, _characterController.velocity.z).magnitude;
        const float speedOffset = 0.1f;
        
        if (currentHorizontalSpeed < targetSpeed - speedOffset
         || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp (currentHorizontalSpeed, targetSpeed * _inputDirection.magnitude, _speedChangeRate * Time.deltaTime);
            _speed = Mathf.Round (_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }
        
        if (_inputDirection != Vector2.zero)
            _targetAngle = Mathf.Atan2 (_inputDirection.x, _inputDirection.y) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
        
        Vector3 targetDirectoin = Quaternion.Euler (0f, _targetAngle, 0f) * Vector3.forward;
        _characterController.Move (_speed * Time.deltaTime * targetDirectoin + new Vector3 (0f, _verticalVelocity * Time.deltaTime, 0f));
    }

    private void JumpAndGravity()
    {
        if (_grounded)
        {
            if (_verticalVelocity < 0f)
                _verticalVelocity = -1f;
        }

        if (Mathf.Abs (_verticalVelocity) < _terminalVelocity)
            _verticalVelocity += _gravity * Time.deltaTime;
    }

    private void GroundedCehck()
    {
        _spherePosition = transform.TransformPoint (_localSpherePosition);
        _grounded = Physics.CheckSphere (_spherePosition, _colliderRadius, _groundLayers);
    }

    private void OnDrawGizmosSelected()
    {
        if (_grounded)
            Gizmos.color = Color.blue;
        else
            Gizmos.color = Color.red;
        
        Gizmos.DrawSphere (_spherePosition, _colliderRadius);
    }
}
