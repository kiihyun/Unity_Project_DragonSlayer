using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class PlayerDashState : PlayerStates
{
    public override void Init(Player owner)
    {
        base.Init(owner);
        state = PlayerState.Dash;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.ChangeAnime(PlayerState.Dash);
        player.Controller.Dash();
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        if (elapsedTime > 0.3f)
        {
            if (player.Controller.CheckPreviousState() is PlayerJumpState)
            {
                player.ChangeAnime(PlayerState.Jump);
            }

            if (player.Controller.IsGrounded())
            {
                player.Controller.IsAttack();

                player.rb.velocity = Vector3.zero;

                if (player.Controller.GetInputDir().x != 0)
                {
                    player.Controller.IsMove();
                }

                else
                {
                    player.Controller.IsStop();
                }
            }
        }
    }
}


