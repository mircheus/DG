using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using DG.Tweening;

[RequireComponent(typeof(TMP_Text))]
public class Glow : MonoBehaviour
{
    [SerializeField] private float _glowDuration = 0.5f;
    private TMP_Text _tmpText;
    
    private void OnEnable()
    {
        if (gameObject.activeSelf)
        {
            Debug.Log(TryGetComponent<TMP_Text>(out TMP_Text text) ? "TMP_text component received" : "ERROR: TMP_text component not received");
            _tmpText = text;
            int ID_GlowPower = Shader.PropertyToID("_GlowPower");
            _tmpText.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0f);
            Tween tween = _tmpText.fontSharedMaterial.DOFloat(1f, ID_GlowPower, _glowDuration).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
