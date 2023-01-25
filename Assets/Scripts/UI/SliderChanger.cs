using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Events;

[RequireComponent(typeof(Slider))]
public class SliderChanger : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    private Slider _slider;
    private Coroutine _sliderCoroutine;
    
    private void OnEnable()
    {
        _player.HealthChanged += MoveSlider;
    }
    
    private void OnDisable()
    {
        _player.HealthChanged -= MoveSlider;
    }
    
    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _player.MaxHealth;
        _slider.value = _player.MaxHealth;
    }
    
    private void MoveSlider(int target)
    {
        if (_sliderCoroutine != null)
        {
            StopCoroutine(_sliderCoroutine);
        }
    
        _sliderCoroutine = StartCoroutine(SliderCoroutine(target));
    }
    
    private IEnumerator SliderCoroutine(int target)
    {
        const float seconds = 0.001f;
        var waitFor = new WaitForSeconds(seconds); 
        int slideDelta = 1; 
    
        while (_slider.value != target)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, target, slideDelta);
            yield return waitFor;
        }
    }
}
