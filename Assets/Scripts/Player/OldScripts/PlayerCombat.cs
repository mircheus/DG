using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// brackeys tutorial 
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _leftAttackPoint;
    [SerializeField] private Transform _rightAttackPoint;
    [SerializeField] private int _damagePoints;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _enemyLayer;
    private Transform _currentAttackPoint;

    private float _horizontalMovement;

    private void Start()
    {
        _currentAttackPoint = _rightAttackPoint;
    }

    private void Update()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        CheckAttackDirection(_horizontalMovement);

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_currentAttackPoint.position, _attackRange, _enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Debug.Log($"we hit an {enemy.name}");
            enemy.GetComponent<Enemy>().TakeDamage(_damagePoints);
        }
    }

    private void CheckAttackDirection(float horizontalMovement)
    {
        if (horizontalMovement < 0)
        {
            _currentAttackPoint = _leftAttackPoint;
        }

        if (horizontalMovement > 0)
        {
            _currentAttackPoint = _rightAttackPoint;
        }
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(_currentAttackPoint.position, _attackRange);
    // }
}