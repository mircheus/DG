using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    public event UnityAction Hitted;
    public event UnityAction<int> HealthChanged;
    public event UnityAction Died;

    private int _currentHealth;
    
    private FlashEffect _damageFX;
    private PlayerController _playerController;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;
    
    private void Start()
    {
        _damageFX = GetComponent<FlashEffect>();
        _playerController = GetComponent<PlayerController>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _damageFX.Blink();
        Hitted?.Invoke();
        HealthChanged(_currentHealth);
        
        if (_currentHealth <= 0)
        {
            Debug.Log("Game Over");
            Died?.Invoke();
            // _playerController.enabled = false; // DISABLED FOR TEST PURPOSES
        }
    }
}
