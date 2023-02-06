using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Patrol))]
public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _range;
    [SerializeField] private float _colliderDistance;   
    [SerializeField] private int _damage;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Ninja _enemy;
    [SerializeField] private Player _player;
    
    private float _cooldownTimer = Mathf.Infinity;
    private Vector2 _colliderOffset;
    private Animator _animator;
    private Patrol _patrol;
    private bool _isAlive = true;
    private Coroutine _attackCooldownCoroutine;
    private int _meleeAttack = Animator.StringToHash("MeleeAttack");
    
    [Header("Debug Variables________________________________/")]
    [SerializeField] private bool _isPlayerInSight;
    [SerializeField] private bool _isCoroutineFinished = false;
    [SerializeField] private bool _isAbleToAttack = true;

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
        _patrol = GetComponentInParent<Patrol>();
    }
    
    private void Update()
    {
        // ===================Вариант через Timer===================
        // _cooldownTimer += Time.deltaTime;
        //
        // if (PlayerInSight())
        // {
        //     if (_cooldownTimer >= _attackCooldown)
        //     {
        //         _cooldownTimer = 0;
        //         _animator.SetTrigger(_meleeAttack);
        //     }
        // }
        // ===================Вариант через Timer===================
        
        // ===================Вариант через Timer EDITED===================
        // _cooldownTimer += Time.deltaTime;
        
        // if (PlayerInSight())
        // {
        //     _animator.SetTrigger(_meleeAttack);
        // }
        // ===================Вариант через Timer EDITED===================

        // ===================Вариант через корутину===================
        if (PlayerInSight())
        {
            if (_isAbleToAttack && _attackCooldownCoroutine == null) 
            {
                _animator.SetTrigger(_meleeAttack);
                _attackCooldownCoroutine = StartCoroutine(CooldownAttack(_attackCooldown));
            }
            else
            {
                if (_isCoroutineFinished)
                {
                    _attackCooldownCoroutine = null;
                }
            }
        }
        // ===================Вариант через корутину===================
        if (_isAlive)
        {
             _patrol.enabled = !PlayerInSight();
        }
        else
        {
            _patrol.enabled = false;
        }
        
        // ========================DEBUG ZONE========================
        _isPlayerInSight = PlayerInSight();
        // ========================DEBUG ZONE========================
    }

    private void OnDied()
    {
        _isAlive = false;
    }

    private IEnumerator CooldownAttack(float cooldownTime)
    {
        _isCoroutineFinished = false;
        _isAbleToAttack = false;
        yield return new WaitForSeconds(cooldownTime);
        _isAbleToAttack = true;
        _isCoroutineFinished = true;
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
 