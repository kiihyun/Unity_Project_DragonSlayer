using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        IsJump();

        base.OnUpdate(deltaTime);
    }

    public Vector3 GetInputDir()
    {
        inputDir.x = Input.GetAxisRaw("Horizontal");
        inputDir.y = Input.GetAxisRaw("Vertical");
        return inputDir;
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
        if (GetInputDir().x != 0)
        {
            ChangeState(nameof(PlayerMoveState));
            return;
        }
    }

    public void IsJump()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeState(nameof(PlayerJumpState));
            return;
        }
    }

    public void IsAttack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeState(nameof(PlayerAttackState));
            return;
        }
    }

    public void Moving()
    {
        Vector3 pos = player.transform.position;
        pos.x += inputDir.normalized.x * 3f * Time.deltaTime;
        player.transform.position = pos;

        player.CharacterImage.flipX = inputDir.x < 0 ? true : false;
    }

    public void Jumping()
    {
        player.rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
    }

    public bool IsGrounded() // 땅에 닿았는지 안닿았는지 확인하는 함수
    {
        LayerMask groundLayer = LayerMask.GetMask("Test");
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.down, 0.8f, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }


}
