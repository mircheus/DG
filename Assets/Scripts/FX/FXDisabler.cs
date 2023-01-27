using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FXDisabler : MonoBehaviour
{
    [SerializeField] private float _seconds = 0.3f;
    private Coroutine _disabler;

    private void OnEnable()
    {
        if (_disabler != null)
        {
            StopCoroutine(_disabler);
        }
        
        _disabler = StartCoroutine(DisableFxIn(_seconds));
    }

    private IEnumerator DisableFxIn(float seconds)
    {
        var waitFor = new WaitForSeconds(seconds);
        yield return waitFor;
        gameObject.SetActive(false);
    }
}
