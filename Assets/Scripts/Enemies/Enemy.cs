using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FlashEffect))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;
    protected EnemyDisabler _enemyDisabler;

    public event UnityAction Died;
    
    protected FlashEffect _damageFx;
    protected int _currentHealth;

    protected virtual void Start()
    {
        _damageFx = GetComponent<FlashEffect>();
        _currentHealth = _maxHealth;
        _enemyDisabler = GetComponentInParent<EnemyDisabler>();
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
    }

    protected virtual void Die()
    {
        Died?.Invoke();
        _enemyDisabler.DisableEnemy(this);
    }
}
