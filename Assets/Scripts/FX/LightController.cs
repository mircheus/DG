using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightController : MonoBehaviour
{
    [SerializeField] private float _duration = 0.1f;
    public UnityEngine.Rendering.Universal.Light2D explosionLight;
    public float explosionLightIntensity;

    private void Start()
    {
        DOVirtual.Float(0, explosionLightIntensity,  _duration, ChangeLight).OnComplete(() => DOVirtual.Float(explosionLightIntensity, 0, .1f, ChangeLight));
    }

    private void ChangeLight(float x)
    {
        explosionLight.intensity = x;
    }
}
