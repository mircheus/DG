using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;

    public event UnityAction Hitted;
    public event UnityAction Died;
    
    private FlashEffect _damageFX;
    private PlayerController _playerController;
    
    private void Start()
    {
        _damageFX = GetComponent<FlashEffect>();
        _playerController = GetComponent<PlayerController>();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _damageFX.Blink();
        Hitted?.Invoke();

        if (_health <= 0)
        {
            Debug.Log("Game Over");
            Died?.Invoke();
            // _playerController.enabled = false; // DISABLED FOR TEST PURPOSES
        }
    }
}
