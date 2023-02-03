using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Turret : Enemy
{
    private SpriteRenderer _spriteRenderer;
    private TurretShooting _turretShooting;
    private Rigidbody2D _rigidbody;
    public event UnityAction<Vector3> Exploded;
    
    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _turretShooting = GetComponent<TurretShooting>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void Die()
    {
        _spriteRenderer.enabled = false;
        _turretShooting.StopFiring();
        _rigidbody.simulated = false;
        Exploded?.Invoke(transform.position);
        base.Die();
    }
}
