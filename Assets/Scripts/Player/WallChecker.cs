using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    private Vector2 _playerPosition;
    private Vector2 _point;
    private Vector2 _difference;
    private Player _player;
    private int _oppositeDirectionToWall;

    public int OppositeDirectionToWall => _oppositeDirectionToWall;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerPosition = _player.transform.position;
        _point = collision.ClosestPoint(transform.position);
        _difference = _playerPosition - _point;
        _oppositeDirectionToWall = (int)Mathf.Sign(_difference.x);
    }
}
