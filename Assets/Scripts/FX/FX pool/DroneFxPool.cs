using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneFxPool : FXPool
{
    [SerializeField] private Drone[] _drones;

    private void OnEnable()
    {
        foreach (var drone in _drones)
        {
            drone.Exploded += EnableFX;
        }
    }

    private void OnDisable()
    {
        foreach (var drone in _drones)
        {
            drone.Exploded -= EnableFX;
        }
    }
}
