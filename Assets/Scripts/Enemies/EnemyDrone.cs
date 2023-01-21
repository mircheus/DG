using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrone : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _nearDistance;
    [SerializeField] private Player _player;

    public float _distanceToPlayer;
    private void Update()
    {
        if (_player == null)
        {
            return;
        }
        
        Chase();
        Flip();
        _distanceToPlayer = Vector2.Distance(_player.transform.position, transform.position);
    }

    private void Chase()
    {
        if(Vector2.Distance(_player.transform.position, transform.position) > _nearDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
        }
        else
        {
            transform.position = transform.position;
        }
    }

    private void Flip()
    {
        if (transform.position.x > _player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
    }

    private void OnDrawGizmos()
    {
        // Gizmos.color = Vector2.Distance(_player.transform.position, transform.position) > _nearDistance ? Color.green : Color.red;
        // Gizmos.DrawLine(_player.transform.position, transform.position);
    }
}
