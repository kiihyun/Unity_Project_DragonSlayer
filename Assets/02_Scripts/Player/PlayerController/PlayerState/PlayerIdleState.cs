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
        player.anim.CrossFade("Idle", 0.1f);
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        player.Controller.IsJump();
        player.Controller.IsDash();
        player.Controller.IsAttack();
        player.Controller.IsMove();
        player.Controller.IsSkill();
        player.Controller.Nonslip();


    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
