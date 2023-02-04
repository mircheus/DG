using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private Pause _pause;

    private int _currentHealth;
    private FlashEffect _damageFX;
    private PlayerController _playerController;
    private PlayerShooting _playerShooting;
    private AnimationSwitcher _animationSwitcher;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;
    
    public event UnityAction Hitted;
    public event UnityAction<int> HealthChanged;
    public event UnityAction Died;

    private void OnEnable()
    {
        _pause.Paused += OnPaused;
        _pause.Unpaused += OnUnpaused;
    }

    private void OnDisable()
    {
        _pause.Paused -= OnPaused;
        _pause.Unpaused -= OnUnpaused;
    }
    
    private void Start()
    {
        Time.timeScale = 1f;
        _damageFX = GetComponent<FlashEffect>();
        _playerController = GetComponent<PlayerController>();
        _playerShooting = GetComponent<PlayerShooting>();
        _animationSwitcher = GetComponent<AnimationSwitcher>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _damageFX.Blink();
        Hitted?.Invoke();
        HealthChanged?.Invoke(_currentHealth);
        
        if (_currentHealth <= 0)
        {
            Died?.Invoke();
            SwitchControlScriptsTo(false);
        }
    }

    private void OnPaused()
    {
        SwitchControlScriptsTo(false);
    }

    private void OnUnpaused()
    {
        SwitchControlScriptsTo(true);
    }

    private void SwitchControlScriptsTo(bool value)
    {
        _playerController.enabled = value;
        _playerShooting.enabled = value;
        _animationSwitcher.enabled = value;
    }
}
