using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class PlayerMoveState : PlayerStates
{
    public override void Init(Player owner)
    {
        base.Init(owner);
        state = PlayerState.Move;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.anim.CrossFade("Run", 0.1f);
        SoundManager.Instance.PlaySFX("PlayerRun2");

    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        player.Controller.IsJump();
        player.Controller.IsStop();
        player.Controller.IsDash();
        player.Controller.IsAttack();
        player.Controller.IsSkill();
        if (elapsedTime > 0.27f)
        {
            SoundManager.Instance.PlaySFX("PlayerRun2");
            elapsedTime = 0f;
        }
    }

    public override void OnFixedUpdate()
    {
        player.Controller.Moving();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

}
