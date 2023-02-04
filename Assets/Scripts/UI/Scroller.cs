using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private float _x, _y;
    [SerializeField] private float _colorChangeDuration = 0.5f;
    [SerializeField] private float _introDelay = 13.4f;
    [SerializeField] private bool _isDisco = false;
    
    private Color[] _colors = new[] {Color.blue, Color.green, Color.magenta, Color.cyan, Color.yellow, Color.white};
    
    private void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.SetDelay(_introDelay, false);
        if (_isDisco)
        {
            for (int i = 0; i < _colors.Length; i++)
            {
                sequence.Append(_image.DOColor(_colors[i], _colorChangeDuration));
            }
        
            sequence.SetEase(Ease.Linear).SetLoops(-1);
        }
    }

    private void Update()
    {   
        _image.uvRect = new Rect(_image.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _image.uvRect.size);
    }
}
