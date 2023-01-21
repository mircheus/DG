using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private PlayerShooting _player;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _shakeDuration = .15f;
    [SerializeField] private float _magnitude = .4f;
    
    private void OnEnable()
    {
        _enemy.Damaged += ShakeCamera;
        _player.Shooted += ShakeCamera;
    }

    private void OnDisable()
    {
        _enemy.Damaged -= ShakeCamera;
        _player.Shooted -= ShakeCamera;
    }

    private void ShakeCamera()
    {
        StopCoroutine(Shake(_shakeDuration, _magnitude));
        StartCoroutine(Shake(_shakeDuration, _magnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);
            elapsed += Time.deltaTime;
            
            yield return null; 
        }

        transform.localPosition = originalPosition;
    }
}
