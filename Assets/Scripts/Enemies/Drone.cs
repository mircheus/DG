using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Drone : Enemy
{
    [SerializeField] private GameObject _deathFx;
    [SerializeField] private Light2D _light;

    private SpriteRenderer _spriteRenderer;
    private EnemyDroneShooting _enemyDroneShooting;
    private Rigidbody2D _rigidbody;

    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // _light = GetComponent<Light2D>();
        _enemyDroneShooting = GetComponent<EnemyDroneShooting>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    protected override void Die()
    {
        Instantiate(_deathFx, transform.position, Quaternion.identity); // заменить на вытащить из пула 
        _spriteRenderer.enabled = false;
        _light.intensity = 0;
        // _enemyDroneShooting.enabled = false;
        _enemyDroneShooting.StopFiring();
        _rigidbody.simulated = false;
        base.Die();
    }
}
