using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonTurning : MonoBehaviour
{
    [SerializeField] private float _turnSmoothTime = 0.12f;

    private PlayerController _playerController;
    
    private GameObject _mainCamera;

    private float _rotationVelocity;
    private Vector2 _inputDirection;
    private int _isAiming;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();

        _mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
    }

    private void OnEnable()
    {
        _playerController.MoveEvent += PlayerController_MoveEvent;
    }

    private void OnDisable()
    {
        _playerController.MoveEvent -= PlayerController_MoveEvent;
    }

    private void OnAttack (InputValue value)
    {
        _isAiming += value.isPressed ? 1 : -1;
    }

    private void OnSkill (InputValue value)
    {
        _isAiming += value.isPressed ? 1 : -1;
    }

    private void Update()
    {
        if (_isAiming != 0)
        {
            float targetAngle = _mainCamera.transform.eulerAngles.y;
            Turn (targetAngle);
        }
        else
        {
            if (_inputDirection != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2 (_inputDirection.x, _inputDirection.y) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                Turn (targetAngle);
            }
        }
    }

    private void PlayerController_MoveEvent (Vector2 direction)
    {
        _inputDirection = direction;
    }

    private void Turn(float targetAngle)
    {
        float rotation = Mathf.SmoothDampAngle (transform.eulerAngles.y, targetAngle, ref _rotationVelocity, _turnSmoothTime);
        transform.rotation = Quaternion.Euler (0f, rotation, 0f);
    }
}
