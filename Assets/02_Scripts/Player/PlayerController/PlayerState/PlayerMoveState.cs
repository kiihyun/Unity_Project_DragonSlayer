using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class PlayerMoveState : PlayerStates
{
    private Vector3 inputDir;

    public override void Init(Player owner)
    {
        base.Init(owner);
        state = PlayerState.Move;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.ChangeAnime(PlayerState.Move);
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        player.Controller.IsStop();

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
