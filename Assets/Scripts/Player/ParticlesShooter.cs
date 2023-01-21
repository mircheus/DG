using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class ParticlesShooter : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    public UnityAction Shooted;
    
    private ParticleSystem _particleSystem;
    private ParticleSystemRenderer _particleSystemRenderer;
    private float _inputX;
    private CinemachineImpulseSource _impulse;
    

    private void Start()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _impulse = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetMouseButtonDown(0))
        {
            _particleSystem.Play();
            _camera.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
            // Shooted?.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _particleSystem.Stop();
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _particleSystemRenderer.gameObject.SetActive(true);
        }
    
        if (_inputX < 0)
        {
            _particleSystemRenderer.flip = Vector3.right;
            Debug.LogWarning($"PSR flip = {_particleSystemRenderer.flip}");
        }
        else if (_inputX > 0)
        {
            _particleSystemRenderer.flip = Vector3.left;
            Debug.LogWarning($"PSR flip = {_particleSystemRenderer.flip}"); 
        }
    }
}
