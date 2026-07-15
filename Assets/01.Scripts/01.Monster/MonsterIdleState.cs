using UnityEngine;

public class MonsterIdleState : IState<MonsterController>
{
    public void Enter(MonsterController monster)
    {
    }

    public void Exit(MonsterController monster)
    {

    }

    public void Update(MonsterController monster)
    {
        if(monster is ITraceable traceMonster)
        {
            monster.stateMachine.ChangeState(monster.mTraceState);
        }
        else
        {
            monster.stateMachine.ChangeState(monster.mMoveState);
        }

    }

    public void FixedUpdate(MonsterController monster)
    {

    }
}
