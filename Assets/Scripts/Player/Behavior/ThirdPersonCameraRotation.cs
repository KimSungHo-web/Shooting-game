using UnityEngine;

public class ThirdPersonCameraRotation : MonoBehaviour
{
    [Header("Cinemachine")]
    [SerializeField] private GameObject _cinemachineCameraTarget;
    [SerializeField] private float _bottomClamp = -40f;
    [SerializeField] private float _topClamp = 70f;
    [SerializeField] private bool _lockCameraPosition = false;
    [SerializeField, Range (0.001f, 1f)] private float mouseXSensitivity = 0.05f;
    [SerializeField, Range (0.001f, 1f)] private float mouseYSensitivity = 0.05f;
    

    [Header ("Recoil")]
    [SerializeField, Range (0f, 7f)] private float recoilAmountY = 6.5f;
    [SerializeField, Range (0f, 3f)] private float recoilAmountX = 2.5f;
    
    private const float _threshold = 0.01f;
    
    private PlayerController _playerController;

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private Vector2 _lookDelta;
    

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        _playerController.LookEvent += PlayerController_LookEvent;
        _playerController.FireEvent += PlayerController_FireEvent;
    }

    private void OnDisable()
    {
        _playerController.LookEvent -= PlayerController_LookEvent;
        _playerController.FireEvent -= PlayerController_FireEvent;
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void PlayerController_LookEvent (Vector2 delta)
    {
        _lookDelta = delta;
    }
    
    private void PlayerController_FireEvent()
    {
        ApplyRecoil();
    }

    private void CameraRotation()
    {
        if (_lookDelta.sqrMagnitude > _threshold && !_lockCameraPosition)
        {
            _cinemachineTargetYaw += _lookDelta.x * mouseXSensitivity;
            _cinemachineTargetPitch += _lookDelta.y * mouseYSensitivity;
        }
        
        _cinemachineTargetYaw = ClampAngle (_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle (_cinemachineTargetPitch, _bottomClamp, _topClamp);
        _cinemachineCameraTarget.transform.rotation = Quaternion.Euler (_cinemachineTargetPitch, _cinemachineTargetYaw, 0f);
    }
    
    private void ApplyRecoil()
    {
        float dx = (Random.value - 0.5f) / 2f * recoilAmountX;
        float dy = (Random.value - 0.5f) / 2f * recoilAmountY;

        _cinemachineTargetYaw += dx;
        _cinemachineTargetPitch += dy;
    }
    
    private static float ClampAngle (float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp (angle, min, max);
    }

    
}
