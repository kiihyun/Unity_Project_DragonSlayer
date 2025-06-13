using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : BaseController<Player>
{
    protected Player player;
    private Vector3 inputDir;
    public bool isDash = false;
    public bool isJump = false;

    public PlayerController(State<Player> initState, Player player) : base(initState, player)
    {
        this.player = player;
    }

    public override void OnUpdate(float deltaTime)
    {
        GetInputDir();
        IsJump();
        IsDash();

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
            return;
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
        if (Input.GetKeyDown(KeyCode.C) && !isJump)
        {
            isJump = true;
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

    public void IsDash()
    {

        if (Input.GetKeyDown(KeyCode.Z) && !isDash)
        {
            isDash = true;
            ChangeState(nameof(PlayerDashState));
            return;
        }
    }

    public void Moving()
    {
        Vector3 pos = player.transform.position;
        pos.x += inputDir.normalized.x * 5f * Time.deltaTime;
        player.transform.position = pos;

        player.CharacterImage.flipX = inputDir.x < 0 ? true : false;
    }

    public void Jumping()
    {
        player.rb.velocity = Vector2.up * 8f;
    }


    public void Dash()
    {
        float dashDir = inputDir.normalized.x != 0 ? Mathf.Sign(inputDir.normalized.x) : player.CharacterImage.flipX ? -1f : 1f;
        player.rb.velocity = new Vector2(dashDir * 10f, 0);
    }

    public bool IsGrounded() // 땅에 닿았는지 안닿았는지 확인하는 함수
    {
        LayerMask groundLayer = LayerMask.GetMask("Test");
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.down, 0.8f, groundLayer);
        if (hit.collider != null)
        {
            isJump = false;
            isDash = false;
            return true;
        }

        return false;
    }


}
