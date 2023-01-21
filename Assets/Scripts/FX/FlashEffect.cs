using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] private Material _blinkMaterial;
    [SerializeField] private float _duration;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Coroutine _flashCoroutine;
    private Material _originalMaterial;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalMaterial = _spriteRenderer.material;
    }

    public void Blink() // переименовать, название странное
    {
        if (_flashCoroutine != null)
        {
            StopCoroutine(_flashCoroutine);
        }

        _flashCoroutine = StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        _spriteRenderer.material = _blinkMaterial;
        yield return new WaitForSeconds(_duration);
        _spriteRenderer.material = _originalMaterial;
        _flashCoroutine = null;
    }

}
