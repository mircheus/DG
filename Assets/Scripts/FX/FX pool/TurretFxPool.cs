using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFxPool : FXPool
{
    [SerializeField] private Turret[] _turrets;
    
    private void OnEnable()
    {
        foreach (var turret in _turrets)
        {
            turret.Exploded += EnableFX;
        }
    }
    
    private void OnDisable()
    {
        foreach (var turret in _turrets)
        {
            turret.Exploded -= EnableFX;
        }
    }
}
