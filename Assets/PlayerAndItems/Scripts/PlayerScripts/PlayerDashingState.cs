using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashingState : PlayerBaseState
{
    // CONTEXT

    private float _dashActiveCounter;
    private Vector2 _dashMovVector;
    

    public PlayerDashingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        _dashMovVector = _ctx.MovementVector;
        _dashActiveCounter = _ctx.DashDuration;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();

        _dashActiveCounter -= Time.deltaTime;
        _ctx.rb.velocity = _dashMovVector * _ctx.DashSpeed * Time.fixedDeltaTime;
    }

    public override void ExitState()
    {
        _ctx.ResetDash();
    }

    public override void CheckSwitchStates()
    {
        if (_dashActiveCounter <= 0)
        {
            SwitchState(_factory.Running());
        }
    }

    public override void InitializeSubState()
    {

    }
}
