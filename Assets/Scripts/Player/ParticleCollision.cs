using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class ParticleCollision : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    public List<ParticleCollisionEvent> collisionEvents;
    public CinemachineVirtualCamera camera;
    [SerializeField] private GameObject _explosionPrefab;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = _particleSystem.GetCollisionEvents(other, collisionEvents);
        
        GameObject explosion = Instantiate(_explosionPrefab, collisionEvents[0].intersection, Quaternion.identity);

        

        // if (other.GetComponent<Rigidbody2D>() != null)
        //     other.GetComponent<Rigidbody2D>().AddForceAtPosition(collisionEvents[0].intersection * 100 - transform.position, collisionEvents[0].intersection + Vector3.up);
    }
}
