using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;

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

        if (_health <= 0)
        {
            Debug.Log("Game Over");
            // _playerController.enabled = false;
        }
    }
}
