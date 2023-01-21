using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeleeEntryState : State
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        State groundEntryState = (State)new GroundEntryState();
        _stateMachine.SetNextState(groundEntryState) ;
    }
}
