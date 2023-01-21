using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FlashEffect))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;

    protected FlashEffect _damageFx;
    protected int _currentHealth;
    public event UnityAction Died;

    protected virtual void Start()
    {
        _damageFx = GetComponent<FlashEffect>();
        _currentHealth = _maxHealth;
    }

    protected virtual void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _damageFx.Blink();

        if (_currentHealth == 0)
        {
            Die();
        }
    }
    
    protected void OnParticleCollision(GameObject other)
    {
        TakeDamage(1);
        // Debug.Log("Collision from enemy draft");
    }

    protected virtual void Die()
    {
        Died?.Invoke();
        // Debug.Log("Died event invoked");
    }
}
