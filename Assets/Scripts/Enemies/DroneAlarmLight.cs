using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DroneAlarmLight : MonoBehaviour
{
    // [SerializeField] private DroneShooting _droneShooting;
    private Player _player;
    private Light2D _droneLight;
    private Color _alarmColor = Color.red;
    
    private void OnEnable()
    {
        GetComponentInParent<DroneShooting>().PlayerDetected += OnPlayerDetected;
    }

    private void OnDisable()
    {
        GetComponentInParent<DroneShooting>().PlayerDetected -= OnPlayerDetected;
    }

    private void Start()
    {
        _droneLight = GetComponent<Light2D>();
        _player = GetComponentInParent<DroneShooting>().Player;
    }

    private void Update()
    {
        DirectLightToPlayer();
    }

    private void OnPlayerDetected()
    {
        DOTween.To(() => _droneLight.color, x => _droneLight.color = x, _alarmColor, 1f);
    }

    private void DirectLightToPlayer()
    {
        Vector2 playerDirection = _player.transform.position - transform.position;

        if (playerDirection.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 120f);
        }
        else if (playerDirection.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 240f);
        } 
        else if (playerDirection.x == 0)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
        }
    }
}
