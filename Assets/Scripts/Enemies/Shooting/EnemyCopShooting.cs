using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Rendering.Universal;

public class EnemyCopShooting : ProjectilePool
{
    // общие поля для Drone и Turret и Cop
    [SerializeField] private Transform _currentShootPoint;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private PlayerDetection _detector;
    [SerializeField] private float _shootPauseDuration;
    [SerializeField] private ParticleSystem _shootFX;
    [SerializeField] private ParticleSystemRenderer _shootFXRenderer;

    // поля которые нужны только для Cop и Drone
    [SerializeField] private Player _player;
    // [SerializeField] private AIPath _aiPath;

    // поля которые нужны только для Cop
    [SerializeField] private Transform _leftShootPoint;
    [SerializeField] private Transform _rightShootPoint;

    private bool _isPlayerDetected;

    private void OnEnable()
    {
        _detector.PlayerDetected += OnPlayerDetected;
        // _aiPath.canMove = false;
    }

    private void OnDisable()
    {
        _detector.PlayerDetected -= OnPlayerDetected;
    }

    private void Start()
    {
        Initialize(_projectilePrefab, _projectileSpeed, _damage);
        OnPlayerDetected();
    }

    private void OnPlayerDetected()
    {
        if (_isPlayerDetected == false)
        {
            _isPlayerDetected = true;
            // _alarmLight.color = Color.red; // сделать плавно через DOTWeen
            // _aiPath.canMove = true; // разрешение на движение должен выдавать отдельный скрипт а не shooting
            StopCoroutine(Firing());
            StartCoroutine(Firing());
        }
    }

    private void ShootProjectile()
    {
        if (TryGetProjectile(out Projectile projectile))
        {
            // _currentShootPoint.position = SetShootPoint();
            projectile.transform.position = _currentShootPoint.position;
            projectile.SetDirection(GetDirectionToPlayer());
            _shootFX.Play();
            EnableObject(projectile);
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

    // private Vector2 SetShootPoint()
    // {
    //     if (_aiPath.desiredVelocity.x > 0)
    //     {
    //         return _rightShootPoint.position;
    //     }
    //     else if (_aiPath.desiredVelocity.x < 0)
    //     {
    //         return _leftShootPoint.position;
    //     }
    //     
    //     return Vector2.zero;
    // }

    private Vector2 GetDirectionToPlayer()
    {
        return (_player.transform.position - _currentShootPoint.position).normalized;
    }
}
