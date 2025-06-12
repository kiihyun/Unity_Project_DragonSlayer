using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : E_StateMachine
{
    public Enemy Enemy { get; private set; }
    public EnemyIdleState IdleState { get; }
    public EnemyChasingState ChasingState { get; }
    public EnemyAttackState AttackState { get; }

    public EnemyStateMachine(Enemy enemy)
    {
        Enemy = enemy;

        IdleState = new EnemyIdleState(this);
        ChasingState = new EnemyChasingState(this);
        AttackState = new EnemyAttackState(this);

        ChangeState(IdleState); // 초기 상태 설정
    }


}
