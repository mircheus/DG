using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashParticles : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    
    private ParticleSystemRenderer _particleSystemRenderer;
    private ParticleSystem _particleSystem;

    private void OnEnable()
    {
        _playerController.Dashed += OnDashed;
    }

    private void OnDisable()
    {
        _playerController.Dashed -= OnDashed;
    }

    private void Start()
    {
        _particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnDashed()
    {
        if (_playerController.HorizontalInputDirection == 1)
        {
            _particleSystemRenderer.flip = Vector3.left;
        }
        else if (_playerController.HorizontalInputDirection <= 0)
        {
            _particleSystemRenderer.flip = Vector3.right;
        }
        
        _particleSystem.Play();
    }
}
