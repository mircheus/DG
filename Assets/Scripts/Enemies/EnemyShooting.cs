using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerDetection))]
public class EnemyShooting : ProjectilePool
{
    [SerializeField] private Transform _currentShootPoint;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private PlayerDetection _detector;
    [SerializeField] private float _shootPauseDuration;
    [SerializeField] private ParticleSystem _shootFX;

    private Coroutine _firingCoroutine;
    private bool _isPlayerDetected = false;
    private bool _isFiring = true;

    protected Transform currentShootPoint => _currentShootPoint;

    public event UnityAction Shooted;

    protected void OnEnable()
    {
        _detector.PlayerDetected += OnPlayerDetected;
    }

    protected void OnDisable()
    {
        _detector.PlayerDetected -= OnPlayerDetected;
    }
    
    protected virtual void Start()
    {
        Initialize(_projectilePrefab, _projectileSpeed, _damage);
    }

    protected virtual void OnPlayerDetected()
    {
        if (_isPlayerDetected == false)
        {
            _isPlayerDetected = true;
            
            if (_firingCoroutine != null)
            {
                StopCoroutine(_firingCoroutine);
            }

            _firingCoroutine = StartCoroutine(Firing());
        }
    }

    private void ShootProjectile(Vector2 shootDirection)
    {
        if (TryGetProjectile(out Projectile projectile))
        {
            projectile.transform.position = _currentShootPoint.position;
            projectile.SetDirection(shootDirection);
            EnableObject(projectile);
            _shootFX.Play();
            Shooted?.Invoke();
        }
    }

    private IEnumerator Firing()
    {
        var waitFor = new WaitForSeconds(_shootPauseDuration);

        while (_isFiring)
        {
            ShootProjectile(GetDirection());
            yield return waitFor;
        }
    }

    public void StopFiring()
    {
        _isFiring = false;
    }

    protected virtual Vector2 GetDirection()
    {
        return Vector2.right;
    }
}
