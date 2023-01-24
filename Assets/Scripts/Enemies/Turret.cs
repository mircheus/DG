using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    [SerializeField] private GameObject _deathFx;
    // private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        // _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Die()
    {
        Instantiate(_deathFx, transform.position, Quaternion.identity); // заменить на вытащить из пула 
        // _spriteRenderer.gameObject.SetActive(false);
        base.Die();
    }
}
