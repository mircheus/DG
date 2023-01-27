using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFxPool : FXPool
{
    [SerializeField] private ParticleCollision _particleCollision;
    
    private void OnEnable()
    {
        _particleCollision.ProjectileCollided += EnableFX;
    }

    private void OnDestroy()
    {
        _particleCollision.ProjectileCollided -= EnableFX;
    }
}
