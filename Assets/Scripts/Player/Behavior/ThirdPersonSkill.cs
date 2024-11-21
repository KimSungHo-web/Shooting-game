using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonSkill : MonoBehaviour
{
    [SerializeField] private Transform _gunBarrelEnd;
    [SerializeField] private GameObject _grenadeRoundPrefab;
    [SerializeField] private Light _faceLight;
    [SerializeField] private float _effectsDisplayTime = 0.2f;
    [SerializeField] private float _fireDelay = 0.3f;

    private ThirdPersonAim _aim;
    private Camera _mainCamera;
    private float _fireDelayCounter;
    private bool _pressingSkillKey;

    private void Awake()
    {
        _aim = GetComponent<ThirdPersonAim>();
        _mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (_fireDelayCounter < _fireDelay)
            _fireDelayCounter += Time.deltaTime;

        if (_pressingSkillKey)
        {
            if (_aim.isAimAligned)
            {
                if (_fireDelayCounter >= _fireDelay)
                {
                    _fireDelayCounter = 0f;
                    Fire();
                }
            }
        }
    }
    
    private void OnSkill(InputValue value)
    {
        _pressingSkillKey = value.isPressed;
    }
    
    private void Fire()
    {
        Vector3 spawnPos = _gunBarrelEnd.position + _mainCamera.transform.forward * 0.1f;
        GameObject obj = Instantiate (_grenadeRoundPrefab, spawnPos, _mainCamera.transform.rotation);
        _faceLight.enabled = true;
        Invoke (nameof(DisableEffects), _effectsDisplayTime);
    }
    
    private void DisableEffects()
    {
       _faceLight.enabled = false;
    }
}
