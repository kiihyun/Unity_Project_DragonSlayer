using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class PlayerAttackState : PlayerStates
{
    public override void Init(Player owner)
    {
        base.Init(owner);
        state = PlayerState.Attack;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.anim.CrossFade("Attack", 0.1f);
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        player.Controller.IsDash();

        if(!player.Controller.IsGrounded())
            player.Controller.IsJump();
        
    }

    public override void OnFixedUpdate()
    {
        if(player.Controller.CheckPreviousState() is PlayerJumpState && !player.Controller.IsGrounded())
        {
            player.Controller.Moving();
        }
    }
}
