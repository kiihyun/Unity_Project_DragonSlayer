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
        Rigidbody2D rb = _boss.Rb;          // 이미 Kinematic + gravityScale 0
        rb.velocity   = Vector2.zero;       // 혹시 남아있을 잔속도 제거

        Vector2 start   = rb.position;      // 점프 시작 지점
        Vector2 target  = _targetPos + new Vector2(Random.Range(-0.5f,0.5f),0);       // 착지 목표
        float   totalT  = _data.LeapTime;   // 체공 시간 (예: 0.8f)
        float   apexY   = _data.LeapApexHeight;  // 최고 높이
        _boss.Animator.SetTrigger("Leap");
        float   elapsed = 0f;
        // FixedUpdate 주기로 부드럽게 이동
        while (elapsed < totalT)
        {
            elapsed += Time.fixedDeltaTime;
            float t01 = Mathf.Clamp01(elapsed / totalT);   // 0 → 1

            // 1) 수평?: 선형 보간 (start → target)
            Vector2 horiz = Vector2.Lerp(start, target, t01);

            // 2) 수직?: 포물선 y = 4h * t(1-t)  (0~h~0)
            float parabolaY = 4f * apexY * t01 * (1f - t01);

            // 3) 최종 위치
            Vector2 nextPos = new Vector2(horiz.x, start.y + parabolaY);

            rb.MovePosition(nextPos);       // Kinematic 이동

            yield return new WaitForFixedUpdate();
        }
        // 착지 후 처리
        OnLand();
        yield return new WaitForSeconds(1.0f);   // 쿨타임
        _boss.StateMachine.ChangeState(new BossIdleState(_boss));
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

