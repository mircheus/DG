using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using Vector2 = UnityEngine.Vector2;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleProjectile;
    [SerializeField] private int _damage;
    [SerializeField] private CinemachineImpulseSource _cameraImpulse;
    [Range(0,1)]
    [SerializeField] private float _slowMoValue;
    [SerializeField] private Volume _volume;

    public int Damage => _damage;
    
    public event UnityAction Shooted;
    public event UnityAction SlowMoActivated;
    public event UnityAction SlowMoDeactivated;

    private void Start()
    {
        _volume.enabled = false;
    }

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
        
        if (Input.GetMouseButtonDown(1))
        {
            ActivateSlowMo(_slowMoValue);
        }

        if (Input.GetMouseButtonUp(1))
        {
            DeactivateSlowMo();
        }
    }

    private void ActivateSlowMo(float value)
    {
        Time.timeScale = value;
        SlowMoActivated?.Invoke();
        _volume.enabled = true;
    }

    private void DeactivateSlowMo()
    {
        Time.timeScale = 1f;
        SlowMoDeactivated?.Invoke();
        _volume.enabled = false;
    }
}
