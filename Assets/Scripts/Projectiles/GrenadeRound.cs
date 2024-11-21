using System;
using UnityEngine;

public class GrenadeRound : MonoBehaviour
{
    [SerializeField] private float _flySpeed = 50f;
    [SerializeField] private int _maxBounceCount = 3;
    [SerializeField] private ParticleSystem _explosionParticleAsset;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _timer = 3f;
    [SerializeField] private AudioClip _launchSfxClip;
    [SerializeField] private AudioClip _explosionSfxClip;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionDamage = 30f;


    private Rigidbody _rb;
    private ParticleSystem _explosionParticle;

    private int _currentBounceCounter;
    private float _timerCounter;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _explosionParticle = Instantiate (_explosionParticleAsset);
    }

    private void Start()
    {
        _rb.AddForce (transform.forward * _flySpeed, ForceMode.Impulse);
    }

    private void Update()
    {
        _timerCounter += Time.deltaTime;
        if (_timerCounter >= _timer)
            ExplosionAndDestroy();
    }

    private void OnCollisionEnter (Collision other)
    {
        LayerMask layer = other.gameObject.layer;

        if (IsLayerMatched (_obstacleLayer, layer))
        {
            _currentBounceCounter += 1;
            if (_currentBounceCounter >= _maxBounceCount)
                ExplosionAndDestroy();
        }
        else if (IsLayerMatched (_enemyLayer, layer))
            ExplosionAndDestroy();
    }

    private bool IsLayerMatched (int mask, int target)
    {
        return mask == (mask | 1 << target);
    }

    private void ExplosionAndDestroy()
    {
        _explosionParticle.transform.position = transform.position;
        _explosionParticle.Stop();
        _explosionParticle.Play();
        
        // apply damage to the enemies
        Collider[] colliders = Physics.OverlapSphere (transform.position, _explosionRadius, _enemyLayer);
        foreach (var hit in colliders)
        {
            if (hit.TryGetComponent<EnemyHealth> (out EnemyHealth health))
                health.TakeDamage ((int)_explosionDamage, hit.transform.position);
        }

        Destroy (gameObject);
    }
}