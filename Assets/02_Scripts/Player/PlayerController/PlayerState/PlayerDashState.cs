using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System.Net.NetworkInformation;

public class PlayerDashState : PlayerStates
{
    private float originalGravityScale;
    public override void Init(Player owner)
    {
        base.Init(owner);
        state = PlayerState.Dash;
        originalGravityScale = player.rb.gravityScale;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.anim.CrossFade("Dash", 0.1f);
        player.stat.CurrentDashCooldown = 0f;
        player.Controller.Dash();
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        player.dashFX.UseDash();
        if (elapsedTime > 0.3f)
        {
            ResetGravity();
            if (player.Controller.PreviousState() is PlayerJumpState)
            {
                player.anim.CrossFade("Jump", 0.1f);
            }

            if (player.Controller.IsGrounded() )
            {
                player.collider.excludeLayers = LayerMask.GetMask("Nothing");
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

    private void ResetGravity()
    {
        player.rb.gravityScale = originalGravityScale;
    }



}


