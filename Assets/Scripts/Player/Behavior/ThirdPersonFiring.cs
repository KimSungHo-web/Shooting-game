using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ThirdPersonFiring : MonoBehaviour
{
    [SerializeField] private GameObject _gunBarrelEnd;
    [SerializeField] private Light _faceLight;
    [SerializeField] private float _effectsDisplayTime = 0.2f;
    [SerializeField] private float _fireRange;
    [SerializeField] private LayerMask _shootableMask;
    [SerializeField] private float _defaultSpreadRange = 5f;
    
    private PlayerController _playerController;
    private Camera _mainCamera;

    private ParticleSystem _gunParticles;
    private LineRenderer _gunLine;
    private AudioSource _gunAudio;
    private Light _gunLight;

    private float _spreadRange;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
        
        _gunParticles = _gunBarrelEnd.GetComponent<ParticleSystem>();
        _gunLine = _gunBarrelEnd.GetComponent<LineRenderer>();
        _gunAudio = _gunBarrelEnd.GetComponent<AudioSource>();
        _gunLight = _gunBarrelEnd.GetComponent<Light>();

        _playerController.FireEvent += PlayerController_FireEvent;
        _playerController.MoveEvent += PlayerController_MoveEvent;

        _spreadRange = _defaultSpreadRange;
    }

    private void PlayerController_FireEvent()
    {
        Fire();
    }
    
    private void PlayerController_MoveEvent (Vector2 vector)
    {
        if (vector == Vector2.zero)
            // not walking
            _spreadRange = _defaultSpreadRange;
        else
            _spreadRange = _defaultSpreadRange * 5f;
    }

    private void Fire()
    {
        _gunAudio.Play();
        
        _gunParticles.Stop();
        _gunParticles.Play();

        _faceLight.enabled = true;
        _gunLight.enabled = true;

        _gunLine.enabled = true;
        _gunLine.SetPosition(0, _gunBarrelEnd.transform.position);
        
        float xSpread = Random.Range (-1f, 1f) * _spreadRange;
        float ySpread = Random.Range (-1f, 1f) * _spreadRange;
        Vector2 screenSpreadPoint = new Vector2 (Screen.width / 2f + xSpread, Screen.height / 2f + ySpread);
        Ray fireRay = _mainCamera.ScreenPointToRay (screenSpreadPoint);
        
        if (Physics.Raycast (fireRay, out RaycastHit hit, _fireRange, _shootableMask))
        {
            _gunLine.SetPosition (1, hit.point);
        }
        else
            _gunLine.SetPosition (1, fireRay.direction * _fireRange + fireRay.origin);
        
        Invoke (nameof(DisableEffects), _effectsDisplayTime);
    }
    
    private void DisableEffects()
    {
       _gunLine.enabled = false;
       _gunLight.enabled = false;
       _faceLight.enabled = false;
    }
    
    
}
