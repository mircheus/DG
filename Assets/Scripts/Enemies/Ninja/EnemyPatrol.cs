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
    private Coroutine _idleCoroutine;
    private Transform _currentTarget;

    [Header("Enemy Animator")] 
    private Animator _animator;
    private int _moving = Animator.StringToHash("Moving");

    private void Awake()
    {
        _initialScale = _enemy.localScale;
        _currentTarget = _rightEdge;
        // _isMovingRight = true;
        _isMovingLeft = true;
        _animator = _enemy.gameObject.GetComponent<Animator>();
    }
    
    private void OnDisable()
    {
        _animator.SetBool(_moving, false);
    }

    private void Start()
    {
        // StartCoroutine(MoveToTarget(1));
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
                // StartCoroutine(StayIdle());
                
            }
        }
    }

    private void MoveAlternative()
    {
        // StartCoroutine(MoveToTarget());
    }
    
    private void DirectionChange()
    {
        // _animator.SetBool(_moving, false);
        // Invoke(nameof(InvertDirection), _idleDuration);
        // _idleTimer += Time.deltaTime;
        if (_idleCoroutine != null)
        {
            StopCoroutine(_idleCoroutine);
        }
        
        _idleCoroutine = StartCoroutine(StayIdle(_idleDuration));


        // _isMovingLeft = !_isMovingLeft;
        // if (_idleTimer > _idleDuration)
        // {
        //     _isMovingLeft = !_isMovingLeft; 
        // }
    }
    
    private void MoveInDirection(int direction)
    {
        // _idleTimer = 0;
        _animator.SetBool(_moving, true);
        _enemy.localScale = new Vector3(Mathf.Abs(_initialScale.x) * direction , _initialScale.y, _initialScale.z);
        var position = _enemy.position;
        position = new Vector3(position.x + _speed * direction * Time.deltaTime, position.y, position.z);
        _enemy.position = position;
    }

    private void InvertDirection()
    {
        _isMovingLeft = !_isMovingLeft;
    }

    private void Stay()
    {
        _animator.SetBool(_moving, false);
    }

    private IEnumerator MoveToTarget(int direction)
    {
        while (Math.Abs(_enemy.position.x - _currentTarget.position.x) > 0.1f)
        {
            // _enemy.position = Vector3.MoveTowards(_enemy.position, _currentTarget.position, 0.1f * Time.deltaTime);
            var position = _enemy.position;
            position = new Vector3(position.x + _speed * direction * Time.deltaTime, position.y, position.z);
            _enemy.position = position;
            yield return null;
        }
        Debug.Log("reached");

        if (_idleCoroutine != null)
        {
            StopCoroutine(_idleCoroutine);
        }

        _idleCoroutine = StartCoroutine(StayIdle(_idleDuration));
    }

    private IEnumerator StayIdle(float time)
    {
        _animator.SetBool(_moving, false);
        Debug.Log("Before");
        yield return new WaitForSeconds(time);
        Debug.Log("after");
        // yield return new WaitForSeconds(1f);
        _isMovingLeft = !_isMovingLeft;
    }
}