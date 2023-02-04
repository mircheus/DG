using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Events;

public class BulletParticle : MonoBehaviour
{
    private int _damage;
    private ParticleSystem _particleSystem;
    private List<ParticleCollisionEvent> collisionEvents;
    
    public int Damage => _damage;
    
    public event UnityAction<Vector3> ProjectileCollided;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _damage = GetComponentInParent<PlayerShooting>().Damage;
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        _particleSystem.GetCollisionEvents(other, collisionEvents);
        ProjectileCollided?.Invoke(collisionEvents[0].intersection);
    }
}
