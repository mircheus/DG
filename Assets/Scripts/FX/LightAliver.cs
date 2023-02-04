using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;


public class LightAliver : MonoBehaviour
{
    [SerializeField] private float _minDuration;
    [SerializeField] private float _maxDuration;
    
    private Light2D _light;
    
    private void Start()
    {
        _light = GetComponent<Light2D>();
        StartCoroutine(FlickerLight(_minDuration, _maxDuration));
    }

    private IEnumerator FlickerLight(float minDuration, float maxDuration)
    {
        while (true)
        {
            _light.enabled = !_light.enabled;
            yield return new WaitForSeconds(Random.Range(minDuration, maxDuration));
        }
    }
}
