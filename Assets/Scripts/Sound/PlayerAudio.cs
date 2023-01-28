using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio samples")]
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private float _shootVolume;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private float _jumpVolume;
    [SerializeField] private AudioClip _dashSound;
    [SerializeField] private float _dashVolume;
    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private float _stepVolume;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private float _hitVolume;
    [SerializeField] private AudioClip _dieSound;
    [SerializeField] private float _dieVolume;

    [Header("Slow mo audio settings")] 
    [Range(0,1)]
    [SerializeField] private float _pitchMultiplier;
    
    [SerializeField] private Player _player;
    [SerializeField] private PlayerShooting _playerShooting;
    [SerializeField] private PlayerController _playerController;

    private AudioSource _audioSource;

    public float PitchMultiplier => _pitchMultiplier;
    private void OnEnable()
    {
        _playerShooting.Shooted += OnShooted;
        _playerShooting.SlowMoActivated += OnSlowMoActivated;
        _playerShooting.SlowMoDeactivated += OnSlowMoDeactivated;
        _playerController.Jumped += OnJumped;
        _playerController.Dashed += OnDashed;
        _player.Hitted += OnHitted;
        _player.Died += OnDied;
    }
    
    private void OnDisable()
    {
        _playerShooting.Shooted -= OnShooted;
        _playerController.Jumped -= OnJumped;
        _playerController.Dashed -= OnDashed;
        _playerShooting.SlowMoActivated -= OnSlowMoActivated;
        _playerShooting.SlowMoDeactivated -= OnSlowMoDeactivated;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnSlowMoActivated()
    {
        _audioSource.pitch = _pitchMultiplier;
        Debug.Log("Pitched down");
    }

    private void OnSlowMoDeactivated()
    {
        _audioSource.pitch = 1f;
        Debug.Log("Pitched up");
    }

    private void OnShooted()
    {
        _audioSource.PlayOneShot(_shootSound, _shootVolume);
    }

    private void OnJumped()
    {
        _audioSource.PlayOneShot(_jumpSound, _jumpVolume);
    }

    private void OnDashed()
    {
        _audioSource.PlayOneShot(_dashSound, _dashVolume);
    }

    private void OnHitted()
    {
        _audioSource.PlayOneShot(_hitSound, _hitVolume);
    }

    private void OnStepped()
    {
        _audioSource.PlayOneShot(_stepSound, _stepVolume);
    }

    private void OnDied()
    {
        _audioSource.PlayOneShot(_dieSound, _dieVolume);
    }
}
