using System;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Cinemachine")]
    public GameObject cinemachineCameraTarget;
    public float topClamp = 75f;
    public float bottomClamp = -40f;
    public bool lockCameraPosition = false;
    
    private ShootingAssetsInput input;
    private GameObject mainCamera;

    private float cinemachineTargetYaw;
    private float cinemachineTargetPitch;
    
    private const float threshold = 0.01f;

    private void Awake()
    {
        input = GetComponent<ShootingAssetsInput>();
        mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void Move()
    {
        
    }

    private void CameraRotation()
    {
        if (input.look.sqrMagnitude > threshold || !lockCameraPosition)
        {
            cinemachineTargetYaw += input.look.x;
            cinemachineTargetPitch += input.look.y;
        }

        cinemachineTargetYaw = ClampAngle (cinemachineTargetYaw, float.MinValue, float.MaxValue);
        cinemachineTargetPitch = ClampAngle (cinemachineTargetPitch, bottomClamp, topClamp);

        cinemachineCameraTarget.transform.rotation = Quaternion.Euler (cinemachineTargetPitch, cinemachineTargetYaw, 0f);
    }

    private static float ClampAngle (float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp (angle, min, max);
    }
}
