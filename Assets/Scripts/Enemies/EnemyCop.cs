using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Pathfinding;
using Path = Pathfinding.Path;

public class EnemyCop : MonoBehaviour
{
    [Header("Pathfinding")] 
    [SerializeField] private Transform _target;
    [SerializeField] private float _activateDistance = 50f;
    [SerializeField] private float _pathUpdateSeconds = .5f;

    [Header("Physics")] 
    [SerializeField] private float _speed = 200f;
    [SerializeField] private float _nextWayPointDistance = 3f;
    [SerializeField] private float _jumpNodeHeightRequirement = 0.8f;
    [SerializeField] private float _jumpModifier = 0.3f;
    [SerializeField] private float _jumpCheckOffset = 0.1f;
    [SerializeField] private float _movementAcceleration;
    [SerializeField] private float _maxMoveSpeed;
    
    [Header("Custom Behaviour")]
    [SerializeField] private bool _followEnabled = true;
    [SerializeField] private bool _jumpEnabled = true;
    [SerializeField] private bool _directionLookEnabled = true;
    
    [Header("Debug variables")]
    private Path _path;
    private int _currentWayPoint = 0;
    public bool _isGrounded;
    private Seeker _seeker;
    private Rigidbody2D _rigidbody;
    // private int i = 0;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _groundChecker;

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        InvokeRepeating("UpdatePath", 0, _pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && _followEnabled)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if (_followEnabled && TargetInDistance() && _seeker.IsDone())
        {
            _seeker.StartPath(_rigidbody.position, _target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (_path == null)
        {
            return;
        }

        if (_currentWayPoint >= _path.vectorPath.Count)
        {
            return;
        }
        
        // See if colliding with anything
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + _jumpCheckOffset);
        // _isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0f);
        _isGrounded = Physics2D.OverlapCircle(startOffset, 0.5f, 3);
        _groundChecker = startOffset;
        // _groundChecker = startOffset + Vector3.down;
        Vector2 direction = ((Vector2)_path.vectorPath[_currentWayPoint] - _rigidbody.position).normalized;
        // Vector2 force = direction * _speed * Time.deltaTime;
        Vector2 force = new Vector2(direction.x, 0) * _movementAcceleration;
        FlipSprite(direction); // WORKAROUND убрать как только будет возможность 

        if (_jumpEnabled && _isGrounded)
        {
            if (direction.y > _jumpNodeHeightRequirement)
            {
                _rigidbody.AddForce(Vector2.up * _speed * _jumpModifier);
            } 
        }
        
        // Movement 
        // !!!! --- вынести в отдельный метод  --- !!!!
        _rigidbody.AddForce(force);
        
        if (Mathf.Abs(_rigidbody.velocity.x) > _maxMoveSpeed)
        {
            _rigidbody.velocity = new Vector2(Mathf.Sign(_rigidbody.velocity.x) * _maxMoveSpeed, _rigidbody.velocity.y);
        }

        //Next Waypoint
        float distance = Vector2.Distance(_rigidbody.position, _path.vectorPath[_currentWayPoint]);
                
        if (distance < _nextWayPointDistance)
        {
            _currentWayPoint++;
        }
    }

    private void FlipSprite(Vector2 direction)
    {
        if (direction.x > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (direction.x < 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, _target.transform.position) < _activateDistance;
    }

    private void OnPathComplete(Path path)
    {
        if (path.error == false)
        {
            _path = path;
            _currentWayPoint = 0;
        }
    }

    private void OnDrawGizmos()
    {   
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Vector2 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + _jumpCheckOffset);
        Gizmos.DrawLine(startOffset, startOffset + Vector2.down);
    }
}
