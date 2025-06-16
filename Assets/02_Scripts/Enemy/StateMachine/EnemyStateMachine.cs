using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : E_StateMachine
{
    public string DebugCurrentState => CurrentState?.GetType().Name;

    public Enemy Enemy { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyChasingState ChasingState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyDeathState DeathState { get; private set; }
    public EnemyGuardState GuardState { get; private set; }
    public EnemyRangedState RangedState { get; private set; }

    public EnemyStateMachine(Enemy enemy)
    {
        Enemy = enemy;

        IdleState = new EnemyIdleState(this);
        ChasingState = new EnemyChasingState(this);
        AttackState = new EnemyAttackState(this);
        DeathState = new EnemyDeathState(this);
        GuardState = new EnemyGuardState(this);
        RangedState = new EnemyRangedState(this);
    }


}
