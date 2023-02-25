using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(BoxCollider2D))]
public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Player player))
        {
            _director.Play();
        }
    }
}
