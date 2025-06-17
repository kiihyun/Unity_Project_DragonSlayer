using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class PlayerSkillState : PlayerStates
{
    public override void Init(Player owner)
    {
        base.Init(owner);
        state = PlayerState.SKill;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.anim.CrossFade("Skill", 0.1f);
        player.stat.CurrentSkillCooldown = 0f;
    }


    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        if (elapsedTime > 0.4f)
        {
            player.energipa.UseSkill();
            player.Controller.IsStop();
            player.Controller.IsMove();
            player.Controller.IsAttack();
            player.Controller.IsJump();
            player.Controller.IsDash();
        }
    }

}
