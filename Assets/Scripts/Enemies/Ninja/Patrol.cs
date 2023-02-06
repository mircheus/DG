using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Util;
using Unity.VisualScripting;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float _leftEdge;
    [SerializeField] private float _rightEdge;
    [SerializeField] private float _speed;
    [SerializeField] private float _idleDuration;
    
    private Vector3 _rightPoint;
    private Vector3 _leftPoint;
    private int _currentDirection = 1;
    private Coroutine _waitCoroutine;
    private Vector3 _initialScale;
    private Animator _animator;
    private int _moving = Animator.StringToHash("Moving");

    private void Start()
    {
        _initialScale = transform.localScale;
        _animator = GetComponent<Animator>();
        var position = transform.position;
        _rightPoint = position + Vector3.right * _rightEdge;
        _leftPoint = position + Vector3.left * _leftEdge;
    }

    private void Update()
    {
        if (IsReachedPatrolPoint(_currentDirection) == false)
        {
            Move(_currentDirection);
        }
        else
        {
            if (_waitCoroutine == null)
            {
                _waitCoroutine = StartCoroutine(WaitThenGoBack(_idleDuration));
            }
        }
    }

    private void Move(int direction)
    {
        _animator.SetBool(_moving, true);
        var position = transform.position;
        position = new Vector3(position.x + _speed * direction * Time.deltaTime, position.y, position.z);
        transform.position = position;
    }

    private void FlipSpriteByX(int direction)
    {
        transform.localScale = new Vector3(Mathf.Abs(_initialScale.x) * direction, _initialScale.y, _initialScale.z);
    }

    private bool IsReachedPatrolPoint(int direction)
    {
        if (direction == 1)
        {
            return transform.position.x >= _rightPoint.x;
        }
        
        if (direction == -1)
        {
            return transform.position.x <= _leftPoint.x;
        }

        return false;
    }

    private IEnumerator WaitThenGoBack(float time)
    {
        _animator.SetBool(_moving, false);
        yield return new WaitForSeconds(time);
        _currentDirection *= -1;
        FlipSpriteByX(_currentDirection);
        _waitCoroutine = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(_leftPoint, Vector3.one);
        Gizmos.DrawWireCube(_rightPoint, Vector3.one);
        var position = transform.position;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(position + Vector3.right * _rightEdge, Vector3.one);
        Gizmos.DrawWireCube(position + Vector3.left * _leftEdge, Vector3.one);
    }
}
