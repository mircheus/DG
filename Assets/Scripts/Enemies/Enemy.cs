using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FlashEffect))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    // [SerializeField] private ParticleSystem _hitFX;
    [SerializeField] private GameObject _explosionFx;
    // [SerializeField] private EnemyPatrol _enemyPatrol;
    
    private FlashEffect _damageFx;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    public event UnityAction Damaged;
    public event UnityAction Died;
    
    private void Start()
    {
        _damageFx = GetComponent<FlashEffect>();
        // _enemyPatrol = GetComponentInParent<EnemyPatrol>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _damageFx.Blink();
        // _animator.SetTrigger("Hurt");
        Damaged?.Invoke();

        if (_health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Died?.Invoke();
        _rigidbody.simulated = false;
        Instantiate(_explosionFx, transform.position, quaternion.identity);
        Destroy(gameObject); // вместо дестроя должен возвращаться в objectPool
    }
}
