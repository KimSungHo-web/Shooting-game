using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ThirdPersonFiring : MonoBehaviour
{
    [SerializeField] private GameObject _gunBarrelEnd;
    [SerializeField] private Light _faceLight;
    [SerializeField] private ParticleSystem _hitParticlesAsset;
    [SerializeField] private ParticleSystem _hitBloodParticlesAsset;
    [SerializeField] private float _effectsDisplayTime = 0.2f;
    [SerializeField] private float _fireRange;
    [SerializeField] private LayerMask _shootableMask;
    [SerializeField] private float _defaultSpreadRange = 5f;
    [SerializeField] private float _fireDelay = 0.3f;

    private ThirdPersonAim _aim;
    private ThirdPersonMovement _movement;
    private PlayerStats _playerStats;
    private Camera _mainCamera;

    private ParticleSystem _gunParticles;
    private LineRenderer _gunLine;
    private AudioSource _gunAudio;
    private Light _gunLight;

    private ParticleSystem _hitParticles;
    private ParticleSystem _hitBloodParticles;

    private float _fireDelayCounter;
    private bool _pressingAttackKey;

    private void Awake()
    {
        _aim = GetComponent<ThirdPersonAim>();
        _movement = GetComponent<ThirdPersonMovement>();
        _playerStats = GetComponent<PlayerStatsHandler>().playerStats;
        _mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
        
        _gunParticles = _gunBarrelEnd.GetComponent<ParticleSystem>();
        _gunLine = _gunBarrelEnd.GetComponent<LineRenderer>();
        _gunAudio = _gunBarrelEnd.GetComponent<AudioSource>();
        _gunLight = _gunBarrelEnd.GetComponent<Light>();

        _hitParticles = Instantiate (_hitParticlesAsset);
        _hitBloodParticles = Instantiate (_hitBloodParticlesAsset);
    }

    private void Update()
    {
        if (_fireDelayCounter < _fireDelay)
            _fireDelayCounter += Time.deltaTime;

        if (_pressingAttackKey)
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

    private void OnAttack (InputValue value)
    {
        _pressingAttackKey = value.isPressed;
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

        float spreadRange = _defaultSpreadRange;
        spreadRange *= _movement.isMoving ? 5f : 1f; 
        
        float xSpread = Random.Range (-1f, 1f) * spreadRange;
        float ySpread = Random.Range (-1f, 1f) * spreadRange;
        Vector2 screenSpreadPoint = new Vector2 (Screen.width / 2f + xSpread, Screen.height / 2f + ySpread);
        Ray fireRay = _mainCamera.ScreenPointToRay (screenSpreadPoint);
        
        if (Physics.Raycast (fireRay, out RaycastHit hit, _fireRange, _shootableMask))
        {
            _gunLine.SetPosition (1, hit.point);

            if (IsLayerMatched (LayerMask.GetMask ("Shootable"), hit.collider.gameObject.layer))
            {
                _hitBloodParticles.transform.position = hit.point;
                _hitBloodParticles.transform.forward = hit.normal;
                _hitBloodParticles.Stop();
                _hitBloodParticles.Play();

                if (hit.collider.gameObject.TryGetComponent (out EnemyHealth enemyHealth))
                {
                    enemyHealth.TakeDamage (_playerStats.attackDamage, hit.point);
                }
            }
            else if (IsLayerMatched (LayerMask.GetMask ("Obstacle"), hit.collider.gameObject.layer))
            {
                _hitParticles.transform.position = hit.point;
                _hitParticles.transform.forward = hit.normal;
                _hitParticles.Play();    
            }
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

    private static bool IsLayerMatched (int mask, int target)
    {
        return mask == (mask | 1 << target);
    }
}
