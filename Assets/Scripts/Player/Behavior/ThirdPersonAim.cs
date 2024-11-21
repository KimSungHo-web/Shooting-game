using UnityEngine;
using UnityEngine.Serialization;

public class ThirdPersonAim : MonoBehaviour
{
    [SerializeField, Range (0f, 1f)]
    private float _aimAlignmentThreshold = 0.9f;
        
    private Camera _mainCamera;

    
    public bool isAimAligned {
        get {
            Vector3 playerHorizontalForward = transform.forward;
            playerHorizontalForward.y = 0f;
            playerHorizontalForward.Normalize();
                
            Vector3 cameraHorizontalForward = _mainCamera.transform.forward;
            cameraHorizontalForward.y = 0f;
            cameraHorizontalForward.Normalize();

            float aimAlignment = Vector3.Dot (playerHorizontalForward, cameraHorizontalForward);

            if (aimAlignment >= _aimAlignmentThreshold)
                return true;

            return false;
        }
    }
    
    
    private void Awake()
    {
        _mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
    }
}
