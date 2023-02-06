using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio samples")]
    [SerializeField] private AudioClip[] _shootSounds;
    [SerializeField] private float _shootVolume;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private float _jumpVolume;
    [SerializeField] private AudioClip _wallJumpSound;
    [SerializeField] private float _wallJumpVolume;
    [SerializeField] private AudioClip _dashSound;
    [SerializeField] private float _dashVolume;
    [SerializeField] private AudioClip[] _stepSounds;
    [SerializeField] private float _stepVolume;
    [SerializeField] private AudioClip[] _hitSounds;
    [SerializeField] private float _hitVolume;
    [SerializeField] private AudioClip _dieSound;
    [SerializeField] private float _dieVolume;

    [Header("Slow mo audio settings")] 
    [Range(0,1)]
    [SerializeField] private float _pitchMultiplier;
    
    [SerializeField] private Player _player;
    [SerializeField] private PlayerShooting _playerShooting;
    [SerializeField] private PlayerMovement _playerMovement;

    private AudioSource _audioSource;
    private int _shootSamplesCount;
    private int _hitSamplesCount;
    private int _stepSamplesCount;
    
    private int _randomIndex;

    public float PitchMultiplier => _pitchMultiplier;
    private void OnEnable()
    {
        _playerShooting.Shooted += OnShooted;
        _playerShooting.SlowMoActivated += OnSlowMoActivated;
        _playerShooting.SlowMoDeactivated += OnSlowMoDeactivated;
        _playerMovement.Jumped += OnJumped;
        _playerMovement.WallJumped += OnWallJumped;
        _playerMovement.Dashed += OnDashed;
        _player.Hitted += OnHitted;
        _player.Died += OnDied;
    }
    
    private void OnDisable()
    {
        _playerShooting.Shooted -= OnShooted;
        _playerMovement.Jumped -= OnJumped;
        _playerMovement.WallJumped -= OnWallJumped;
        _playerMovement.Dashed -= OnDashed;
        _playerShooting.SlowMoActivated -= OnSlowMoActivated;
        _playerShooting.SlowMoDeactivated -= OnSlowMoDeactivated;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _shootSamplesCount = _shootSounds.Length;
        _hitSamplesCount = _hitSounds.Length;
        _stepSamplesCount = _stepSounds.Length;
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
        int randomIndex = Random.Range(0, _shootSamplesCount);
        _audioSource.PlayOneShot(_shootSounds[randomIndex], _shootVolume);
    }

    private void OnJumped()
    {
        _audioSource.PlayOneShot(_jumpSound, _jumpVolume);
    }

    private void OnWallJumped()
    {
        _audioSource.PlayOneShot(_wallJumpSound, _wallJumpVolume);
    }

    private void OnDashed()
    {
        _audioSource.PlayOneShot(_dashSound, _dashVolume);
    }

    private void OnHitted()
    {
        int randomIndex = Random.Range(0, _hitSamplesCount);
        _audioSource.PlayOneShot(_hitSounds[randomIndex], _hitVolume);
    }

    private void OnStepped()
    {
        int randomIndex = Random.Range(0, _stepSamplesCount);
        _audioSource.PlayOneShot(_stepSounds[randomIndex], _stepVolume);
    }

    private void OnDied()
    {
        _audioSource.PlayOneShot(_dieSound, _dieVolume);
    }
}
