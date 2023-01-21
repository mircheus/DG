using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Path_old : MonoBehaviour
{
    [SerializeField] private Vector3[] _waypoints;
    [SerializeField] private int _duration = 5;
    public bool error { get; set; }

    private void Start()
    {
        Tween tween = transform.DOPath(_waypoints, _duration, PathType.Linear,PathMode.Sidescroller2D).SetOptions(true).SetLookAt(0);
        tween.SetEase(Ease.Linear).SetLoops(-1);
    }
}
