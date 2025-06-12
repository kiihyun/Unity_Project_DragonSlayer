using Enums;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
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


        ControllerRegister();
    }

    public void ControllerRegister()
    {
        controller = new PlayerController(new PlayerIdleState(), this);
        controller.RegisterState(new PlayerMoveState(), this);
        controller.RegisterState(new PlayerJumpState(), this);

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

}
