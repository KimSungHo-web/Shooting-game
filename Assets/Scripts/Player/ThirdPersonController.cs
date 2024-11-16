using System;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    private const float threshold = 0.01f;
    
    private static float ClampAngle (float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp (angle, min, max);
    }

    [Header ("Player")]
    public float moveSpeed = 5f;
    public float turnSmoothTime = 0.12f;
    
    [Header("Cinemachine")]
    public GameObject cinemachineCameraTarget;
    public float topClamp = 75f;
    public float bottomClamp = -40f;
    public bool lockCameraPosition = false;

    // player
    private float rotationVelocity;
    
    // cinemachine
    private float cinemachineTargetYaw;
    private float cinemachineTargetPitch;

    private Animator animator;
    private ShootingAssetsInput input;
    private CharacterController controller;
    private GameObject mainCamera;
    

    private void Awake()
    {
        input = GetComponent<ShootingAssetsInput>();
        controller = GetComponent<CharacterController>();
        mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
    }

    private void Update()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void Move()
    {
        if (input.move != Vector2.zero)
        {
            Debug.Log(input.move);
            Vector3 inputDirection = new Vector3 (input.move.x, 0f, input.move.y).normalized;
            float targetAngle = Mathf.Atan2 (inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle (transform.eulerAngles.y, targetAngle, ref rotationVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler (0f, rotation, 0f);

            Vector3 moveDirection = Quaternion.Euler (0f, targetAngle, 0f) * Vector3.forward;
            controller.Move (moveSpeed * Time.deltaTime * moveDirection);
        }
    }

    private void CameraRotation()
    {
        if (input.look.sqrMagnitude > threshold && !lockCameraPosition)
        {
            cinemachineTargetYaw += input.look.x;
            cinemachineTargetPitch += input.look.y;
        }

        cinemachineTargetYaw = ClampAngle (cinemachineTargetYaw, float.MinValue, float.MaxValue);
        cinemachineTargetPitch = ClampAngle (cinemachineTargetPitch, bottomClamp, topClamp);

        cinemachineCameraTarget.transform.rotation = Quaternion.Euler (cinemachineTargetPitch, cinemachineTargetYaw, 0f);
    }


}
