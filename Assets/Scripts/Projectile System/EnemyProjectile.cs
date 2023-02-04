using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
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
