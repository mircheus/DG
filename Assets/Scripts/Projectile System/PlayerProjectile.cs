using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{ 
    private void OnEnable()
    {
        // _rigidbody = GetComponent<Rigidbody2D>();
        // _rigidbody.velocity = (Vector2.up) * _speed;
        // Debug.Log("Player projectile enabled");
    }
    protected override void OnCollisionEnter2D(Collision2D col)
    {   
        base.OnCollisionEnter2D(col);
        
        if (col.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            ReturnToPool();
        }
    }
}
