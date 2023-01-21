using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _panel;
    // [SerializeField] private Collider2D _collider2D;

    private void OnTriggerEnter2D(Collider2D col)
    {
        _button.gameObject.SetActive(true);
    }

    public void ShowPanel()
    {
        _panel.SetActive(true);
    }
}
