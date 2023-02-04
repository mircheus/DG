using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform _leftEdge;
    [SerializeField] private Transform _rightEdge;

    [Header("Patrol Points")] 
    [SerializeField] private Transform _enemy;
    
    [Header("Movement parameters")]
    [SerializeField] private float _speed;

    [Header("Idle Behaviour")]
    [SerializeField] private float _idleDuration;

    private float _idleTimer;
    private bool _isMovingLeft;
    private bool _isMovingRight;
    private Vector3 _initialScale;
    
    [Header("Enemy Animator")] 
    private Animator _animator; 

    private void Awake()
    {
        _initialScale = _enemy.localScale;
        _animator = _enemy.gameObject.GetComponent<Animator>();
    }
    
    private void OnDisable()
    {
        _animator.SetBool("Moving", false);
    }
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_isMovingLeft)
        {
            if (_enemy.position.x >= _leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (_enemy.position.x <= _rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            { 
                DirectionChange();
            }
        }
    }
    
    private void DirectionChange()
    {
        _animator.SetBool("Moving", false);
        _idleTimer += Time.deltaTime;

        if (_idleTimer > _idleDuration)
        {
            _isMovingLeft = !_isMovingLeft; 
        }
    }
    
    private void MoveInDirection(int direction)
    {
        _idleTimer = 0;
        _animator.SetBool("Moving", true);
        _enemy.localScale = new Vector3(Mathf.Abs(_initialScale.x) * direction , _initialScale.y, _initialScale.z);
        _enemy.position = new Vector3(_enemy.position.x + _speed * direction * Time.deltaTime, _enemy.position.y, _enemy.position.z);
    }
}