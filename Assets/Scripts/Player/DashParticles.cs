using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashParticles : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    
    private ParticleSystemRenderer _particleSystemRenderer;
    private ParticleSystem _particleSystem;

    private void OnEnable()
    {
        _playerMovement.Dashed += OnDashed;
    }

    private void OnDisable()
    {
        _playerMovement.Dashed -= OnDashed;
    }

    private void Start()
    {
        _particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnDashed()
    {
        if (_playerMovement.HorizontalInputDirection == 1)
        {
            _particleSystemRenderer.flip = Vector3.left;
        }
        else if (_playerMovement.HorizontalInputDirection <= 0)
        {
            _particleSystemRenderer.flip = Vector3.right;
        }
        
        _particleSystem.Play();
    }
}
