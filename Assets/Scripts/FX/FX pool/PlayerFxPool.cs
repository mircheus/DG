using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFxPool : FXPool
{
    [SerializeField] private BulletParticle _bulletParticle;
    
    private void OnEnable()
    {
        _bulletParticle.ProjectileCollided += EnableFX;
    }

    private void OnDestroy()
    {
        _bulletParticle.ProjectileCollided -= EnableFX;
    }
}
