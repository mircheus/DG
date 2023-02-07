using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class DroneShooting : EnemyShooting
{
    [SerializeField] private Player _player;
    [SerializeField] private AIPath _aiPath;

    public event UnityAction PlayerDetected;
    public Player Player => _player;

    protected override void Start()
    {
        base.Start();
        _aiPath.canMove = false;
    }

    protected override void OnPlayerDetected()
    {
        base.OnPlayerDetected();
        _aiPath.canMove = true;
        PlayerDetected?.Invoke();
    }
    
    protected override Vector2 GetDirection()
    {
        return (_player.transform.position - currentShootPoint.position).normalized;
    }
}
