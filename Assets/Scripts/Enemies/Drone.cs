using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Enemy
{
    [SerializeField] private GameObject _deathFx;

    protected override void Die()
    {
        Instantiate(_deathFx, transform.position, Quaternion.identity); // заменить на вытащить из пула 
        base.Die();
    }
}
