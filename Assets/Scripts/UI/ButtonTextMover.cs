using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTextMover : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private int _yOffset;

    public void OnPointerDown(PointerEventData eventData)
    {
        _rectTransform.position -= new Vector3(0, _yOffset, 0);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _rectTransform.position += new Vector3(0, _yOffset, 0);
    }
}
