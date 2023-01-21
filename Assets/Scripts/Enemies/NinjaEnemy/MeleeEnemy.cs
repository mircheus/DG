using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _range;
    [SerializeField] private float _colliderDistance;   
    [SerializeField] private int _damage;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private LayerMask _playerLayer;

    private Player _player;
    private float _cooldownTimer = Mathf.Infinity;
    private Vector2 _colliderOffset;
    private Animator _animator;
    private EnemyPatrol _enemyPatrol;
    private bool _isAlive = true;
    private Enemy _enemy; // чтобы подписаться на событие 

    private void OnEnable()
    {
        _enemy.Died += OnDied;
    }

    private void OnDisable()
    {
        _enemy.Died -= OnDied;
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyPatrol = GetComponentInParent<EnemyPatrol>();
        _enemy = GetComponent<Enemy>();
    }
    
    private void Update()
    {
        _cooldownTimer += Time.deltaTime;
        
        if (PlayerInSight())
        {
            if (_cooldownTimer >= _attackCooldown)
            {
                _cooldownTimer = 0;
                _animator.SetTrigger("MeleeAttack");
            }
        }

        if (_enemyPatrol != null)
        {
            if (_isAlive) // WORKAROUND для того чтобы прекратить движение после смерти противника
            {
                _enemyPatrol.enabled = !PlayerInSight();
            }
            else
            {
                _enemyPatrol.enabled = false;
            }
        }
    }

    private void OnDied()
    {
        _animator.SetBool("Die",true);
        _isAlive = false;
        Debug.Log("Died");
    }

    private void ChasePlayer()
    {
        
    }

    private bool PlayerInSight()
    {
        Vector3 size = new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z);
        _colliderOffset = _boxCollider.bounds.center + transform.right * _range * Mathf.Sign(transform.localScale.x) *_colliderDistance;
        RaycastHit2D hit = Physics2D.BoxCast(_colliderOffset, size, 0, Vector2.left, 0, _playerLayer);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out Player player))
            {
                _player = player;
            }
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Vector3 size = new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z);
        Vector2 colliderOffset = _boxCollider.bounds.center + transform.right * _range * Mathf.Sign(transform.localScale.x) * _colliderDistance;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(colliderOffset, size); 
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {   
            _player.TakeDamage(_damage);
        }
    }
}
 