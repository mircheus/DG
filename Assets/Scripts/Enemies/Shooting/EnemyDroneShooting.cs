using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pathfinding;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyDroneShooting : ProjectilePool
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
    
    // поля которые нужны только Drone 
    [SerializeField] private Player _player;
    [SerializeField] private AIPath _aiPath;
    [SerializeField] private Light2D _alarmLight;

    private Light _light;

    private Vector2 _shootDirection;
    private bool _isPlayerDetected = false;
    private void OnEnable()
    {
        _detector.PlayerDetected += OnPlayerDetected;
        _aiPath.canMove = false;
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
        if (_isPlayerDetected == false)
        {
            _isPlayerDetected = true;
            Color alarmColor = Color.red;
            DOTween.To(() => _alarmLight.color, x => _alarmLight.color = x, alarmColor, 0.5f);
            _aiPath.canMove = true; // разрешение на движение должен выдавать отдельный скрипт а не shooting
            StopCoroutine(Firing());
            StartCoroutine(Firing());
        }
    }

    private void ShootProjectile()
    {
        if (TryGetProjectile(out Projectile projectile))
        {
            projectile.transform.position = _currentShootPoint.position;
            projectile.SetDirection(GetDirectionToPlayer());
            
            // Vector3 playerDirection = (_player.transform.position - transform.position).normalized;
            Vector3 playerDirection = -(_player.transform.position - transform.position).normalized;
            // Vector3 playerDirection = (transform.position - _player.transform.position).normalized;
            // Vector3 playerDirection = -(transform.position - _player.transform.position).normalized;
            
            float rotationZ = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
            
            // _alarmLight.transform.localRotation = Quaternion.Euler(0f, 0f, rotationZ);
            _alarmLight.transform.localRotation = Quaternion.Euler(0f, 0f, -rotationZ);

            _shootFX.Play();
            EnableObject(projectile);
        }
    }
    
    // private void SetFXRotation
    
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

    private Vector2 GetDirectionToPlayer()
    {
        return (_player.transform.position - _currentShootPoint.position).normalized;
    }
}
