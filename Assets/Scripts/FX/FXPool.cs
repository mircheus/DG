using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FXPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;
    [SerializeField] private ParticleCollision _particleCollision;
    [SerializeField] private ParticleSystem _fx;
    
    private Queue<ParticleSystem> _pool;
    // [SerializeField] private int _eachFxCount;
    // [SerializeField] private Dictionary<string, ParticleSystem> _allFx;

    private void OnEnable()
    {
        _particleCollision.ProjectileCollided += EnableFX;
    }

    private void OnDestroy()
    {
        _particleCollision.ProjectileCollided -= EnableFX;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
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

    private bool TryGetFX(out ParticleSystem fx)
    {
        fx = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);
        return fx != null;
    }

    public void EnableFX(Vector3 position)
    {
        if (TryGetFX(out ParticleSystem fx))
        {
            fx.gameObject.transform.position = position;
            fx.gameObject.SetActive(true);
        }
    }
}
