using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FXPool : MonoBehaviour
{
    [SerializeField] protected GameObject _container;
    [SerializeField] protected int _capacity;
    [SerializeField] protected ParticleCollision _particleCollision;
    [SerializeField] protected ParticleSystem _fx;

    protected Queue<ParticleSystem> _pool;
    
    private void Start()
    {
        Initialize();
    }

    protected void Initialize()
    {
        _pool = new Queue<ParticleSystem>();
        
        for(int i = 0; i < _capacity; i++)
        {
            ParticleSystem spawned = Instantiate(_fx, _container.transform);
            spawned.gameObject.SetActive(false);
            _pool.Enqueue(spawned);
        }
        
        Debug.Log("FX pool initialized");
    }

    protected bool TryGetFX(out ParticleSystem fx)
    {
        fx = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);
        return fx != null;
    }

    protected void EnableFX(Vector3 position)
    {
        if (TryGetFX(out ParticleSystem fx))
        {
            fx.gameObject.transform.position = position;
            fx.gameObject.SetActive(true);
        }
    }
}
