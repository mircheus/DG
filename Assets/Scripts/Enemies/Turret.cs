using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : EnemyDRAFT
{
    [SerializeField] private GameObject _deathFx;

    protected override void Die()
    {
        base.Die();
        Instantiate(_deathFx, transform.position, Quaternion.identity); // заменить на вытащить из пула 
    }

    protected override void TakeDamage(int damage)
    {
        base.TakeDamage(1);
    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        TakeDamage(1);
        Debug.Log("Collision from turret script");
    }
}
