using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyDroneFlip : MonoBehaviour
{
    [SerializeField] private AIPath _aiPath;

    private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // мой вариант через флип
        if (_aiPath.desiredVelocity.x >= 0.01f)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_aiPath.desiredVelocity.x <= -0.01f)
        {
            _spriteRenderer.flipX = false;
        }
    }
}
