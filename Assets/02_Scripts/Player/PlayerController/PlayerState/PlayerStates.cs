using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class PlayerStates : State<Player>
{
    protected PlayerState state;
    protected Player player;

    public override void Init(Player owner)
    {
        this.player = owner;
    }

    public PlayerState GetState()
    {
        return state;
    }
}
