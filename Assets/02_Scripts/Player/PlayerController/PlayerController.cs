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
    public bool isDead = false;
    public bool isSkill = false;


    public PlayerController(State<Player> initState, Player player) : base(initState, player)
    {
        this.player = player;
    }

    public override void OnUpdate(float deltaTime)
    {
        GetInputDir();
        
        IsDead();

        IsInteract();

        DashCoolTime();
        SkillCoolTime();

        ItemUse();
        ItemSwap();

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

    public void IsSkill()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSkill)
        {
            isSkill = true;
            ChangeState(nameof(PlayerSkillState));
            return;
        }
    }

    public void ItemUse()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("ItemUse");
            player.inventory.UseConsumable();
        }
    }

    // 아이템 위치 스왑
    public void ItemSwap()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            player.inventory.swapQuickSlotItem();
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
        player.rb.velocity = new Vector2(
            inputDir.normalized.x * player.stat.MoveSpeed,
            player.rb.velocity.y
        );
        player.CharacterImage.flipX = inputDir.x < 0 ? true : false;



        //Vector3 pos = player.transform.position;
        //pos.x += inputDir.normalized.x * player.stat.MoveSpeed * Time.deltaTime;
        //player.transform.position = pos;

        //player.CharacterImage.flipX = inputDir.x < 0 ? true : false;
    }

    public void Jumping()
    {
        SoundManager.Instance.PlaySFX("PlayerJump");
        player.rb.velocity = Vector2.up * player.stat.JumpPower;
    }


    public void Dash()
    {
        SoundManager.Instance.PlaySFX("PlayerDash");
        float dashDir = inputDir.normalized.x != 0 ? Mathf.Sign(inputDir.normalized.x) : player.CharacterImage.flipX ? -1f : 1f;
        player.rb.gravityScale = 0.1f; 
        player.collider.excludeLayers = LayerMask.GetMask("Enemy");
        player.rb.velocity = new Vector2(dashDir * player.stat.DashPower, 0);
    }

    public void Nonslip()
    {
        if (IsOnSlope() && inputDir.x == 0 && !isDash && !isJump)
        {
            player.rb.velocity = Vector2.zero;
        }
    }

    public void DashCoolTime()
    {
        if (player.stat.CurrentDashCooldown >= player.stat.DashCooldown)
        {
            isDash = false;
        }

        else
        {
            player.stat.CurrentDashCooldown += Time.deltaTime;
        }
    }

    public void SkillCoolTime()
    {
        if (player.stat.CurrentSkillCooldown >= player.stat.SkillCooldown)
        {
            isSkill = false;
        }
        else
        {
            player.stat.CurrentSkillCooldown += Time.deltaTime;
        }
    }



    public bool IsGrounded() 
    {
        LayerMask groundLayer = LayerMask.GetMask("Test");
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.down, 1f, groundLayer);
        if (hit.collider != null)
        {
            isJump = false;
            return true;
        }

        return false;
    }

    // 경사면 체크 함수
    private bool IsOnSlope()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Test", "Ground"));
        if (hit)
        {
            // 경사 각도 계산
            float angle = Vector2.Angle(hit.normal, Vector2.up);
            return angle > 0.1f && angle <= 60;
        }
        return false;
    }

}
