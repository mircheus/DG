using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using Vector2 = UnityEngine.Vector2;

public class PlayerShooting : MonoBehaviour
{
    // удалить как появится новый репозиторий в гите 
    // [SerializeField] private Transform _rightShootPoint;
    // [SerializeField] private Transform _leftShootPoint;
    // [SerializeField] private Transform _shootPoint;
    // [SerializeField] private Projectile _projectilePrefab;
    // [SerializeField] private float _projectileSpeed;
    // [SerializeField] private int _damage;
    [SerializeField] private ParticleSystem _particleProjectile;
    [SerializeField] private CinemachineImpulseSource _cameraImpulse;

    public event UnityAction Shooted;
    
    // удалить как появится новый репозиторий в гите 
    private void Start()
    {
        // Initialize(_projectilePrefab, _projectileSpeed, _damage);
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ShootInDirection(_shootPoint.right, _shootPoint);
            _cameraImpulse.GenerateImpulse();
            _particleProjectile.Play(); // сделать отдельный метод, чтобы можно было передавать Damage
        }

        if (Input.GetMouseButtonUp(0))
        {
            _particleProjectile.Stop();
        }
    }
    
    // удалить как появится новый репозиторий в гите 
    // private void ShootInDirection(Vector2 direction, Transform shootPoint)
    // {
    //     if (TryGetProjectile(out Projectile projectile))
    //     {
    //         projectile.transform.position = shootPoint.position;
    //         projectile.SetDirection(direction);
    //         EnableObject(projectile);
    //         Shooted?.Invoke();
    //     }
    // }

    private void ShootParticleProjectileInDirection()
    {
        _particleProjectile.Play();
    }
}
