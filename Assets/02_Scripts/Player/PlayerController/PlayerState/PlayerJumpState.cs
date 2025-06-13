using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Unity.VisualScripting;

public class PlayerJumpState : PlayerStates
{
    public override void Init(Player owner)
    {
        base.Init(owner);
        state = PlayerState.Jump;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.ChangeAnime(PlayerState.Jump);
        player.Controller.Jumping();
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        player.Controller.IsDash();
        player.Controller.IsAttack();

        if (elapsedTime > 0.5f)
        {
            if (player.Controller.IsGrounded())
            {
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

    public override void OnFixedUpdate()
    {
        player.Controller.Moving();
    }


}
