using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FlashEffect))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    
    private EnemyDisabler _enemyDisabler;
    private FlashEffect _damageFx;
    private int _currentHealth;
    
    public event UnityAction Died;
    public event UnityAction Hitted;
    
    protected virtual void Start()
    {
        _damageFx = GetComponent<FlashEffect>();
        _enemyDisabler = GetComponentInParent<EnemyDisabler>();
        _currentHealth = _maxHealth;
    }

    protected virtual void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _damageFx.Blink();
        Hitted?.Invoke();

        if (_currentHealth == 0)
        {
            Die();
        }
    }

    private void Foo()
    {
        
    }
    
    protected void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out BulletParticle playerBulletParticle))
        {
            TakeDamage(playerBulletParticle.Damage);
        }
    }

    protected virtual void Die()
    {
        Died?.Invoke();
        _enemyDisabler.DisableEnemy(this);
    }
}
