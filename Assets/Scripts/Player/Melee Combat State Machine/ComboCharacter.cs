using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{
    private StateMachine _meleeStateMachine;

    [SerializeField] public Collider2D hitBox;
    [SerializeField] public GameObject hitEffect;
    [SerializeField] private ParticleSystem _hitFX;

    public ParticleSystem HitFX => _hitFX;
    private void Start()
    {
        _meleeStateMachine = GetComponent<StateMachine>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
        {
            _meleeStateMachine.SetNextState(new GroundEntryState());
        }
    }
}
