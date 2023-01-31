using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    [SerializeField] private Vector2 _player;
    [SerializeField] private Vector2 _point;
    [SerializeField] private Vector2 _difference;
    [SerializeField] private float _differenceX;
    private Player _playerPosition;

    private void Start()
    {
        _playerPosition = GetComponentInParent<Player>();
        _differenceX = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = _playerPosition.transform.position;
        _point = collision.ClosestPoint(transform.position);
        Debug.Log("Collided with trigger");
        _difference = _player - _point;
        _differenceX = _difference.x;
    }

    public int GetDirection()
    {
        var sign = Mathf.Sign(_differenceX);
        return (int)sign;
    }
}
