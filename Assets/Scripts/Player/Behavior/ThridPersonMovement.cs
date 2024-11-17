using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ThridPersonMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _turnSmoothTime = 0.12f;

    private ThirdPersonController _thirdPersonController;
    private CharacterController _characterController;
    private GameObject _mainCamera;

    private float _rotationVelocity;
    private Vector2 _movementDirection;

    private void Awake()
    {
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _characterController = GetComponent<CharacterController>();

        _mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
    }

    private void OnEnable()
    {
        _thirdPersonController.MoveEvent += ThirdPersonController_OnMoveEvent;
    }

    private void OnDisable()
    {
        _thirdPersonController.MoveEvent -= ThirdPersonController_OnMoveEvent;
    }

    private void Update()
    {
        Move();
    }

    private void ThirdPersonController_OnMoveEvent (Vector2 direction)
    {
        _movementDirection = direction;
    }

    private void Move()
    {
        if (_movementDirection != Vector2.zero)
        {
            // turn
            float targetAngle = Mathf.Atan2 (_movementDirection.x, _movementDirection.y) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle (transform.eulerAngles.y, targetAngle, ref _rotationVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler (0f, rotation, 0f);

            // move
            Vector3 moveDirection = Quaternion.Euler (0f, targetAngle, 0f) * Vector3.forward;
            _characterController.Move (_moveSpeed * Time.deltaTime * moveDirection);
        }
    }

}
