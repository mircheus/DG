using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlowMoListener : MonoBehaviour
{
    [SerializeField] private PlayerShooting _playerShooting;

    public event UnityAction SlowMoActivated;
    public event UnityAction SlowMoDeactivated;
    private void OnEnable()
    {
        _playerShooting.SlowMoActivated += OnSlowMoActivated;
        _playerShooting.SlowMoDeactivated += OnSlowMoDeactivated;
    }

    private void OnDisable()
    {
        _playerShooting.SlowMoActivated -= OnSlowMoActivated;
        _playerShooting.SlowMoDeactivated -= OnSlowMoDeactivated;
    }

    private void OnSlowMoActivated()
    {
        SlowMoActivated?.Invoke();
    }

    private void OnSlowMoDeactivated()
    {
        SlowMoDeactivated?.Invoke();
    }
}
