using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerDetection))]
public class EnemyShooting : ProjectilePool
{
    [SerializeField] protected Transform _currentShootPoint;
    [SerializeField] protected Projectile _projectilePrefab;
    [SerializeField] protected float _projectileSpeed;
    [SerializeField] protected int _damage;
    [SerializeField] protected PlayerDetection _detector;
    [SerializeField] protected float _shootPauseDuration;
    [SerializeField] protected ParticleSystem _shootFX;

    protected Coroutine _firingCoroutine;
    protected bool _isPlayerDetected = false;
    protected bool _isFiring = true;

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

    protected void ShootProjectile(Vector2 shootDirection)
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

    protected IEnumerator Firing()
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
