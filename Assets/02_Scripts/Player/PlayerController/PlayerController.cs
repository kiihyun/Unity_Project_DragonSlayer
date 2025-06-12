using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController<Player>
{
    protected Player player;
    private Vector3 inputDir;

    public PlayerController(State<Player> initState, Player player) : base(initState,player)
    {
        this.player = player;
    }

    public override void OnUpdate(float deltaTime)
    {
        GetInputDir();

        base.OnUpdate(deltaTime);
    }

    public Vector3 GetInputDir()
    {
        inputDir.x = Input.GetAxisRaw("Horizontal");
        inputDir.y = Input.GetAxisRaw("Vertical");
        return inputDir;
    }

    public void Moving()
    {
        player.transform.position += inputDir.normalized * 3f * Time.deltaTime;
        player.CharacterImage.flipX = inputDir.x < 0 ? true : false;
    }

    public void IsStop()
    {
        if (GetInputDir() == Vector3.zero)
        {
            ChangeState(nameof(PlayerIdleState));
        }
    }

    public void IsMove()
    {
        if (GetInputDir() != Vector3.zero)
        {
            ChangeState(nameof(PlayerMoveState));
            return;
        }
    }


}
