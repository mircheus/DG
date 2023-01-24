using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTurretShooting : ProjectilePool
{
    // [SerializeField] private Transform _rightShootPoint;
    // [SerializeField] private Transform _leftShootPoint;
    
    // общие поля для Drone и Turret
    [SerializeField] private Transform _currentShootPoint;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private PlayerDetection _detector;
    [SerializeField] private float _shootPauseDuration;
    [SerializeField] private ParticleSystem _shootFX;
    [SerializeField] private ParticleSystemRenderer _shootFXRenderer;
    
    // поля которые нужны только Turret
    [Range(-1, 1)] 
    [SerializeField] private int _shootDirection;

    public event UnityAction Shooted;

    private void OnEnable()
    {
        _detector.PlayerDetected += OnPlayerDetected;
    }
    
    private void OnDisable()
    {
        _detector.PlayerDetected -= OnPlayerDetected;
    }

    private void Start()
    {
        Initialize(_projectilePrefab, _projectileSpeed, _damage);
    }
    
    private void OnPlayerDetected()
    {
        StopCoroutine(Firing());
        StartCoroutine(Firing());
    }

    private void ShootProjectile()
    {
        if (TryGetProjectile(out Projectile projectile))
        {
            projectile.transform.position = _currentShootPoint.position;
            projectile.SetDirection(transform.right * _shootDirection);
            EnableObject(projectile);
            _shootFX.Play();
            Shooted?.Invoke();
        }
    }

    private IEnumerator Firing()
    {
        var waitFor = new WaitForSeconds(_shootPauseDuration);
        int i = 0;
        
        while (i < 100)
        {
            ShootProjectile();
            yield return waitFor;
        }
    }
}
