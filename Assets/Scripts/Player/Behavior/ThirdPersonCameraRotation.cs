using System;
using UnityEngine;
using UnityEngine.Serialization;


public class ThirdPersonCameraRotation : MonoBehaviour
{
    [SerializeField] private GameObject _cinemachineCameraTarget;
    [SerializeField] private float _bottomClamp = -40f;
    [SerializeField] private float _topClamp = 70f;
    [SerializeField] private bool _lockCameraPosition = false;
    
    private const float _threshold = 0.01f;
    
    private ThirdPersonController _thirdPersonController;

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private Vector2 _lookDelta;

    private void Awake()
    {
        _thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void OnEnable()
    {
        _thirdPersonController.LookEvent += ThirdPersonController_OnLookEvent;
    }

    private void OnDisable()
    {
        _thirdPersonController.LookEvent -= ThirdPersonController_OnLookEvent;
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void ThirdPersonController_OnLookEvent (Vector2 delta)
    {
        _lookDelta = delta;
    }

    private void CameraRotation()
    {
        if (_lookDelta.sqrMagnitude > _threshold && !_lockCameraPosition)
        {
            _cinemachineTargetYaw += _lookDelta.x;
            _cinemachineTargetPitch += _lookDelta.y;
        }
        
        _cinemachineTargetYaw = ClampAngle (_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle (_cinemachineTargetPitch, _bottomClamp, _topClamp);
        _cinemachineCameraTarget.transform.rotation = Quaternion.Euler (_cinemachineTargetPitch, _cinemachineTargetYaw, 0f);
    }
    
    private static float ClampAngle (float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp (angle, min, max);
    }
}
