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
    public bool isAttack = false;
    public IInteract InteractObject;
    public GameObject InteractObjectUI;

    public PlayerController(State<Player> initState, Player player) : base(initState, player)
    {
        this.player = player;
    }

    public override void OnUpdate(float deltaTime)
    {
        GetInputDir();
        
        IsDead();

        IsInteract();

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
        if (Input.GetKeyDown(KeyCode.X) && !isAttack)
        {
            ChangeState(nameof(PlayerAttackState));
            return;
        }
    }

    public void IsDead()
    {
        if(player.stat.CurrentHealth <= 0)
        {
            ChangeState(nameof(PlayerDeathState));
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

    public void IsInteract()
    {
        if(InteractObjectUI != null)
        {
            if(InteractObject != null)
            {
                InteractObjectUI.SetActive(true);
            }
            else
            {
                InteractObjectUI.SetActive(false);
            }   
        }
        
        if (Input.GetKeyDown(KeyCode.E) && InteractObject != null)
        {
            InteractObject.Interact();
        }
    }

    public void Moving()
    {
        Vector3 pos = player.transform.position;
        pos.x += inputDir.normalized.x * player.stat.MoveSpeed * Time.deltaTime;
        player.transform.position = pos;

        player.CharacterImage.flipX = inputDir.x < 0 ? true : false;
    }

    public void Jumping()
    {
        player.rb.velocity = Vector2.up * player.stat.JumpPower;
    }


    public void Dash()
    {
        float dashDir = inputDir.normalized.x != 0 ? Mathf.Sign(inputDir.normalized.x) : player.CharacterImage.flipX ? -1f : 1f;
        player.rb.velocity = new Vector2(dashDir * player.stat.DashPower, 0);
    }

    public void LerfStop()
    {
        Vector2 currentVelocity = player.rb.velocity;
        Vector2 targetVelocity = Vector2.zero;
        float smoothFactor = 0.1f;

        player.rb.velocity = Vector2.Lerp(currentVelocity, targetVelocity, smoothFactor);
    }

    


    public bool IsGrounded() // ���� ��Ҵ��� �ȴ�Ҵ��� Ȯ���ϴ� �Լ�
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
