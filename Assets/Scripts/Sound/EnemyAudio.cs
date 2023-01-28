using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAudio : MonoBehaviour
{
    protected AudioSource _audioSource;
    protected SlowMoListener _slowMoListener;
    
    protected void Awake()
    {
        _slowMoListener = GetComponentInParent<SlowMoListener>();
    }

    protected void OnEnable()
    {
        _slowMoListener.SlowMoActivated += OnSlowMoActivated;
        _slowMoListener.SlowMoDeactivated += OnSlowMoDeactivated;
    }

    protected void OnDisable()
    {
        _slowMoListener.SlowMoActivated -= OnSlowMoActivated;
        _slowMoListener.SlowMoDeactivated -= OnSlowMoDeactivated;
    }

    protected void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        // Debug.Log(_audioSource == null);
    }

    private void OnSlowMoActivated()
    {
        Debug.Log("Subscribed successfully");
        _audioSource.pitch = 0.5f; // TEMPORARYLY VALUE
    }

    private void OnSlowMoDeactivated()
    {
        _audioSource.pitch = 1f;
    }
}
