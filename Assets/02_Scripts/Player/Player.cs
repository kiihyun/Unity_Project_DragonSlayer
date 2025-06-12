using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController Controller { get { return controller; } }
    private PlayerController controller;

    public PlayerStat stat;

    public Animator anim;

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


        ControllerRegister();
    }

    public void ControllerRegister()
    {
        controller = new PlayerController(new PlayerIdleState(), this);
        controller.RegisterState(new PlayerMoveState(), this);
        
    }

}
