using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : Enemy
{
    private Animator _animator;
    private int _hurt = Animator.StringToHash("Hurt");
    private int _die = Animator.StringToHash("Die");
    private Rigidbody2D _rigidbody;

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        Debug.Log(_currentHealth);
    }
    

    protected override void TakeDamage(int damage)
    {
        base.TakeDamage(1);
        _animator.SetTrigger(_hurt);
        // Debug.Log("ninja damaged");
    }

    protected override void Die()
    {
        base.Die();
        _animator.SetTrigger(_die);
        _rigidbody.simulated = false;
    }
}
