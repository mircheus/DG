using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEntryState : MeleeBaseState
{
    public override void OnEnter(StateMachine stateMachine)
    {
        base.OnEnter(stateMachine);
        // Attack
        _attackIndex = 1;
        Duration = 0.5f;
        _animator.ResetTrigger("Attack3");
        _animator.SetTrigger("Attack" + _attackIndex);
        Debug.Log("Player Attack" + _attackIndex + " fired!");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= Duration)
        {
            if (_shouldCombo)
            {
                stateMachine.SetNextState(new GroundComboState());
            }
            else
            {
                stateMachine.SetNextStateToMain();
            }
        }
    }
}
