using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private float _shootVolume;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private float _jumpVolume;
    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private float _stepVolume;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private float _hitVolume;
    [SerializeField] private AudioClip _dieSound;
    [SerializeField] private float _dieVolume;
    
    
    [SerializeField] private Player _player;
    [SerializeField] private PlayerShooting _playerShooting;
    [SerializeField] private PlayerController _playerController;

    private AudioSource _audioSource;
    
    private void OnEnable()
    {
        _playerShooting.Shooted += OnShooted;
        _playerController.Jumped += OnJumped;
        _player.Hitted += OnHitted;
        _player.Died += OnDied;
    }
    
    private void OnDisable()
    {
        _playerShooting.Shooted -= OnShooted;
        _playerController.Jumped -= OnJumped;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnShooted()
    {
        _audioSource.PlayOneShot(_shootSound, _shootVolume);
    }

    private void OnJumped()
    {
        _audioSource.PlayOneShot(_jumpSound, _jumpVolume);
    }

    private void OnHitted()
    {
        _audioSource.PlayOneShot(_hitSound, _hitVolume);
    }

    private void OnDied()
    {
        _audioSource.PlayOneShot(_dieSound, _dieVolume);
    }
}
