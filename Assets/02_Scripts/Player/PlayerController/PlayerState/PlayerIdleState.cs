using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class PlayerIdleState : PlayerStates
{
    public override void Init(Player owner)
    {
        base.Init(owner);
        state = PlayerState.Idle;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        player.Controller.IsMove();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
