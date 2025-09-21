using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class PlayerDeathState : PlayerStates
{
    public override void Init(Player owner)
    {
        base.Init(owner);
        state = PlayerState.Death;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.anim.CrossFade("Death", 0.1f);
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
    }


}
