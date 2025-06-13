using Enums;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController Controller { get { return controller; } }
    private PlayerController controller;

    [HideInInspector]
    public PlayerStat stat;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Rigidbody2D rb;
    
    public SpriteRenderer CharacterImage { get { return characterImage; } }
    private SpriteRenderer characterImage;


    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        controller?.OnUpdate(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        controller?.OnFixedUpdate();
    }

    private void Init()
    {
        characterImage ??= GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        stat = GetComponent<PlayerStat>();
        rb = GetComponent<Rigidbody2D>();   
        stat.StartStat();
        ControllerRegister();
    }

    public void ControllerRegister()
    {
        controller = new PlayerController(new PlayerIdleState(), this);
        controller.RegisterState(new PlayerMoveState(), this);
        controller.RegisterState(new PlayerJumpState(), this);
        controller.RegisterState(new PlayerDashState(), this);
        controller.RegisterState(new PlayerAttackState(), this);
    }

    public void ChangeAnime(PlayerState nextAnime)
    {
        if (nextAnime == PlayerState.Death)
        {
            anim.SetTrigger("IsDead");
        }
        else
        {
            anim.SetInteger("ChangeState", (int)nextAnime);
        }
    }

    public void OnAttackEnd()
    {
        Debug.Log("Attack Ended!");

        if(controller.GetInputDir().x != 0)
        {
            controller.IsMove();
        }
        else
        {
            controller.IsStop();
        }
    }

    public void OnAttackHit()
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f, LayerMask.GetMask("TestEnemy"));

        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy.TryGetComponent<IDamageble>(out IDamageble target))
            {
                Debug.Log("Hit!");
                target.TakeDamage(10);
            }
        }
    }

}
