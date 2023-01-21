using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FXDestroyer : MonoBehaviour
{
    [SerializeField] private float _destroyIn = 0.3f;
    private void Start()
    {
        Destroy(gameObject, _destroyIn);
    }
}
