using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    private void Update()
    {
        ShootProjectileManually();
    }

    private void OnPlayerDetected()
    {
        StopCoroutine(Firing());
        StartCoroutine(Firing());
        // ShootProjectile();
    }

    private void ShootProjectile()
    {
        if (TryGetProjectile(out Projectile projectile))
        {
            projectile.transform.position = _currentShootPoint.position; 
            // Debug.Log($"position x:{projectile.transform.position.x} y:{projectile.transform.position.y}");
            projectile.SetDirection(transform.right * _shootDirection);
            // Debug.Log($"projectile speed: {projectile.Speed}");
            // Debug.Log($"projectile current direction: x:{projectile.CurrentDirection.x} y:{projectile.CurrentDirection.y}");
            EnableObject(projectile);
            _shootFX.Play();
            Debug.Log("Projectile activated");
        }
    }

    private void InstantiateProjectile()
    {
        Projectile projectile = Instantiate(_projectilePrefab, _currentShootPoint.position, Quaternion.identity);
        // Debug.Log($"projectile shooted at position X:{_currentShootPoint.position.x} Y:{_currentShootPoint.position.y}");
        projectile.SetDirection(Vector2.right * -1); 
        projectile.Initialize(0, 0);
    }

    private void ShootProjectileManually()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShootProjectile();
        }   
    }

    private IEnumerator Firing()
    {
        var waitFor = new WaitForSeconds(_shootPauseDuration);
        int i = 0;
        
        while (i < 100)
        {
            ShootProjectile();
            // Debug.Log("Fired");
            yield return waitFor;
        }
    }
}
