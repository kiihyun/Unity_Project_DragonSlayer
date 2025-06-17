using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLeapState : IBossState
{
    readonly BossEnemy _boss;
    readonly BossSkillData _data;
    Vector2 _targetPos;

    public BossLeapState(BossEnemy b, BossSkillData d) { _boss = b; _data = d; }

    public void Enter()
    {
        // ① 착지 목표 = 현재 플레이어 위치
        _targetPos = _boss.PlayerTarget
            ? _boss.PlayerTarget.position
            : _boss.transform.position + Vector3.right * 4f;

        // ② 바로 점프
        _boss.StartCoroutine(LeapRoutine());
    }

    public void Execute() { }
    public void Exit()    { }

    IEnumerator LeapRoutine()
    {
        // ‘대기’ 없음 → 곧바로 발사
        Rigidbody2D rb = _boss.Rb;
        Vector2  start = rb.position;
        Vector2  diff  = _targetPos - start;
        float       t  = _data.LeapTime;               // 체공 시간(예: 0.8 s)

        rb.velocity = new Vector2(diff.x / t,
            2f * _data.LeapApexHeight / t);

        // (애니메이션 필요 없으면 아래 한 줄 삭제)
        _boss.Animator.Play("Jump");

        yield return new WaitUntil(() => _boss.IsGrounded());
        OnLand();
    }

    void OnLand()
    {

        foreach (var c in Physics2D.OverlapCircleAll(
                     _boss.transform.position,
                     _data.LandRadius,
                     LayerMask.GetMask("Player")))
            if (c.TryGetComponent<IDamageble>(out var d))
                d.TakeDamage(_data.LandDamage);

        _boss.StartCoroutine(BackToIdle());
    }

    IEnumerator BackToIdle()
    {
        yield return new WaitForSeconds(1.0f);   // 쿨타임
        _boss.StateMachine.ChangeState(new BossIdleState(_boss));
    }
}

