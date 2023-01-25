using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FXPool : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fxPrefab;

    private ObjectPool<ParticleSystem> _pool;

    private void Start()
    {
        _pool = new ObjectPool<ParticleSystem>(
            () =>
            {
                return Instantiate(_fxPrefab);
            },
            fxPrefab =>
            {
                fxPrefab.gameObject.SetActive(true);
            },
            fxPrefab =>
            {
                fxPrefab.gameObject.SetActive(false);
            },
            fxPrefab =>
            {
                Destroy(fxPrefab.gameObject);
            }, true, 20, 50);
    }
}
