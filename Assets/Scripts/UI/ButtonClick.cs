using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _default, _pressed, _selected;
    [SerializeField] private AudioClip _pressedClip;
    [SerializeField] private AudioClip _highlightClip;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private int _yOffset = 15;
    
    private RectTransform x;
    private TextMeshProUGUI y;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _image.sprite = _pressed;
        _audioSource.PlayOneShot(_pressedClip);
        _rectTransform.position -= new Vector3(0, _yOffset, 0);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _image.sprite = _default;
        _rectTransform.position += new Vector3(0, _yOffset, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.sprite = _selected;
        _audioSource.PlayOneShot(_highlightClip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.sprite = _default;
    }
}