using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FXPool : MonoBehaviour
{
    private GameObject _container;
    [SerializeField] protected int _capacity;
    [SerializeField] protected FX _fx;

    protected Queue<FX> _pool;
    
    private void Start()
    {
        _container = gameObject;
        Initialize();
    }

    private void Initialize()
    {
        _pool = new Queue<FX>();
        
        for(int i = 0; i < _capacity; i++)
        {
            FX spawned = Instantiate(_fx, _container.transform);
            spawned.gameObject.SetActive(false);
            _pool.Enqueue(spawned);
        }
    }

    private bool TryGetFX(out FX fx)
    {
        fx = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);
        return fx != null;
    }

    protected void EnableFX(Vector3 position)
    {
        if (TryGetFX(out FX fx))
        {
            fx.gameObject.transform.position = position;
            fx.gameObject.SetActive(true);
        }
    }
}
