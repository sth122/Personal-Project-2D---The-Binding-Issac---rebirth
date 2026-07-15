using UnityEngine;

public class MonsterIdleState : IState
{
    MonsterController controller;

    public MonsterIdleState(MonsterController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
    }

    public void Exit()
    {

    }

    public void Update()
    {
        if(controller is ITraceable traceMonster)
        {
            controller.stateMachine.ChangeState(controller.mTraceState);
        }
        else
        {
            controller.stateMachine.ChangeState(controller.mMoveState);
        }
    }

    public void FixedUpdate()
    {

    }
}
