using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;
    
    private readonly Queue<Projectile> _pool = new Queue<Projectile>();

    protected void Initialize(Projectile prefab, float speed, int damage)
    {
        for (int i = 0; i < _capacity; i++)
        {
            Projectile spawned = Instantiate(prefab, _container.transform);
            spawned.Initialize(speed, damage);
            spawned.gameObject.SetActive(false);
            _pool.Enqueue(spawned);
        }
    }

    protected bool TryGetProjectile(out Projectile result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }
    
    protected void EnableObject(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
    }
    
    
    
}
