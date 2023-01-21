using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    private void OnEnable()
    {
        // _rigidbody = GetComponent<Rigidbody2D>();
        // _rigidbody.velocity = (Vector2.up) * _speed;
        // Debug.Log("turret projectile enabled");
    }

    protected override void OnCollisionEnter2D(Collision2D col) 
    {
        base.OnCollisionEnter2D(col);
        
        if (col.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
            ReturnToPool();
        }
    }
    
    
}
