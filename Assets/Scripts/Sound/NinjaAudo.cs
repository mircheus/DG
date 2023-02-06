using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NinjaAudo : MonoBehaviour
{
    [SerializeField] private AudioClip _swordAttackSound;
    [SerializeField] private float _swordAttackVolume;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private float _hitVolume;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private float _deathVolume;

    [SerializeField] private Ninja _ninja;
    private MeleeAttack _meleeAttack;
    private AudioSource _audioSource;
    
    private void OnEnable()
    {
        _ninja.Hitted += OnHitted;
        _ninja.Died += OnDied;
    }

    private void OnDisable()
    {
        _ninja.Hitted -= OnHitted;
        _ninja.Died -= OnDied;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnHitted()
    {
        _audioSource.PlayOneShot(_hitSound, _hitVolume);
    }

    private void OnDied()
    {
        _audioSource.PlayOneShot(_deathSound, _deathVolume);
    }

    private void OnAttacked()
    {
        _audioSource.PlayOneShot(_swordAttackSound, _swordAttackVolume);
    }
}
