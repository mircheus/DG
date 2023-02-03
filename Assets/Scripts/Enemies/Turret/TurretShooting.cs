using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : EnemyShooting
{
    [Header("Shoot Side (left -1, right 1)")]
    [Range(-1, 1)] 
    [SerializeField] private int _shootSide;

    protected override Vector2 GetDirection()
    {
        return transform.right * _shootSide; 
    }
}
