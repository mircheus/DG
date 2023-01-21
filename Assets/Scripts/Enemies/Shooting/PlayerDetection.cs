using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetection : MonoBehaviour
{
    public event UnityAction PlayerDetected;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Player player))
        {
            PlayerDetected?.Invoke();
            Debug.Log("Player detected");
        }
    }
}

