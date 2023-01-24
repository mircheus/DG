using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TurretAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private float _shootVolume;
    [SerializeField] private AudioClip _turnSound;
    [SerializeField] private float _turnVolume;
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private float _explosionVolume;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private float _hitVolume;
    
    [SerializeField] private EnemyTurretShooting _enemyTurretShooting;
    [SerializeField] private Turret _turret;

    private AudioSource _audioSource;
    private void OnEnable()
    {
        _enemyTurretShooting.Shooted += OnShooted;
        _turret.Hitted += OnHitted;
        _turret.Died += OnDied;
    }

    private void OnDisable()
    {
        _enemyTurretShooting.Shooted -= OnShooted;
        _turret.Hitted -= OnHitted;
        _turret.Died -= OnDied;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnShooted()
    {
        _audioSource.PlayOneShot(_shootSound, _shootVolume);
    }

    private void OnHitted()
    {
        _audioSource.PlayOneShot(_hitSound, _hitVolume);
    }
    
    private void OnDied()
    {
        Debug.Log("Method invoked");
        _audioSource.PlayOneShot(_explosionSound, _explosionVolume);
    }
}
