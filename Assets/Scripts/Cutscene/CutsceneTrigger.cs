using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(BoxCollider2D))]
public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;

    public event UnityAction cutsceneStarted;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Player player))
        {
            _director.Play();
            cutsceneStarted?.Invoke();
        }
    }
}
