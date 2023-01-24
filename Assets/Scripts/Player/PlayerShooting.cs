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
    [SerializeField] private ParticleSystem _particleProjectile;
    [SerializeField] private CinemachineImpulseSource _cameraImpulse;
    
    public event UnityAction Shooted;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _cameraImpulse.GenerateImpulse();
            _particleProjectile.Play(); 
            Shooted?.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _particleProjectile.Stop();
        }
    }
    
    private void ShootParticleProjectileInDirection()
    {
        _particleProjectile.Play();
    }
}
