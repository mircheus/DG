using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NinjaAudo : MonoBehaviour
{
    [SerializeField] private AudioClip _swordAttackSound;
    [SerializeField] private float _swordAttackVolume;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private float _hitVolume;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private float _deathVolume;
}
