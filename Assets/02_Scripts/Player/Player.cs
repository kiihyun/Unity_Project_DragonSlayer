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
    [HideInInspector]
    public DashFX dashFX;
    [HideInInspector]
    public Collider2D collider;
    [HideInInspector]
    public Energipa energipa;

    public SpriteRenderer CharacterImage { get { return characterImage; } }
    private SpriteRenderer characterImage;
    public Inventory inventory;

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
        characterImage = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        stat = GetComponent<PlayerStat>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        stat.Init();
        dashFX = GetComponentInChildren<DashFX>();
        ControllerRegister();
        inventory = GetComponent<Inventory>();
        energipa = GetComponentInChildren<Energipa>();
    }

    public void ControllerRegister()
    {
        controller = new PlayerController(new PlayerIdleState(), this);
        controller.RegisterState(new PlayerMoveState(), this);
        controller.RegisterState(new PlayerJumpState(), this);
        controller.RegisterState(new PlayerDashState(), this);
        controller.RegisterState(new PlayerAttackState(), this);
        controller.RegisterState(new PlayerDeathState(), this);
        controller.RegisterState(new PlayerSkillState(), this);
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

        if (controller.GetInputDir().x != 0)
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

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f, LayerMask.GetMask("Enemy"));

        foreach (var hit in hits)
        {

            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy == null)
            {
                BossEnemy boss = hit.GetComponent<BossEnemy>();
                if(boss.TryGetComponent<IDamageble>(out IDamageble target) && target != null)
                {
                    target.TakeDamage(stat.AttackPower);
                    break;
                }
            }

            if (enemy == null)
            {
                hit.AddComponent<Enemy>();

                if (enemy.TryGetComponent<IDamageble>(out IDamageble target) && target != null)
                {
                    target.TakeDamage(stat.AttackPower);
                }
                

            }
        }
    }

    public IEnumerator Hit()
    {
        if (controller.CurrentState() is PlayerDeathState)
            yield break;

        characterImage.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        characterImage.color = Color.white;

    }

}
