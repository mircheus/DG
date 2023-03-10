using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DroneAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private float _shootVolume;
    [SerializeField] private AudioClip _flySound;
    [SerializeField] private float _flyVolume;
    [SerializeField] private AudioClip _alarmSound;
    [SerializeField] private float _alarmVolume;
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private float _explosionVolume;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private float _hitVolume;
    [SerializeField] private DroneShooting _droneShooting;
    [SerializeField] private PlayerDetection _playerDetection;
    [SerializeField] private Drone _drone;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _droneShooting.Shooted += OnShooted;
        _playerDetection.PlayerDetected += OnAlarmed;
        _drone.Died += OnDied;
        _drone.Hitted += OnHitted;
    }

    private void OnDisable()
    {
        _droneShooting.Shooted -= OnShooted;
        _playerDetection.PlayerDetected -= OnAlarmed;
        _drone.Died -= OnDied;
        _drone.Hitted -= OnHitted;
    }
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnShooted()
    {
        _audioSource.PlayOneShot(_shootSound, _shootVolume);
    }

    private void OnAlarmed()
    {
        _audioSource.PlayOneShot(_alarmSound, _alarmVolume);
    }

    private void OnDied()
    {
        _audioSource.PlayOneShot(_explosionSound, _explosionVolume);
    }

    private void OnHitted()
    {
        _audioSource.PlayOneShot(_hitSound, _hitVolume);
    }
}
