using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightAliver : MonoBehaviour
{
    private Light2D _light;

    private void Start()
    {
        _light = GetComponent<Light2D>();
    }

    private void Update()
    {
        DOVirtual.Float(0f, 1.5f, 1f, ChangeLight);
        // DOVirtual.Float(0, 1.5f, 0.5f,ChangeLight).OnComplete(() => DOVirtual.Float())
            
    }
    // DOVirtual.Float(0, explosionLightIntensity,  _duration, ChangeLight).OnComplete(() => DOVirtual.Float(explosionLightIntensity, 0, .1f, ChangeLight));
    private void ChangeLight(float x)
    {
        _light.intensity = x;
    }
}
