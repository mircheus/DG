using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    protected float _speed;
    protected int _damage;
    protected Rigidbody2D _rigidbody;
    private Vector2 _currentDirection;

    public float Speed => _speed;
    public Vector2 CurrentDirection => _currentDirection;

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out Ground ground))
        {
            Debug.Log("Collided with ground");
            ReturnToPool();
        }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        Move();
    }

    public void Initialize(float speed, int damage)
    {
        _speed = speed;
        _damage = damage;
    }

    public void SetDirection(Vector2 direction)
    {
        _currentDirection = direction;
    }

    private void Move()
    {
        _rigidbody.velocity = _currentDirection * _speed;
    }
    
    protected void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
