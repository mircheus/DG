using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class Drone : Enemy
{
    private Light2D _light;
    private SpriteRenderer _spriteRenderer;
    private DroneShooting _droneShooting;
    private Rigidbody2D _rigidbody;

    public event UnityAction<Vector3> Exploded; 
    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _droneShooting = GetComponent<DroneShooting>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _light = GetComponentInChildren<Light2D>();
    }
    protected override void Die()
    {
        _spriteRenderer.enabled = false;
        _light.intensity = 0;
        _droneShooting.StopFiring();
        _rigidbody.simulated = false;
        Exploded?.Invoke(transform.position);
        base.Die();
    }
}
