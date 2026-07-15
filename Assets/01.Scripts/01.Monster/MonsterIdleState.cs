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
        Debug.Log("IdleState에 진입");
        // Appear Anim 실행
        // Anim 끝날 때 까지 대기 후 실행
    }

    public void Exit()
    {
        Debug.Log("IdleState에 퇴장");
    }

    public void Update()
    {
        if(controller is ITraceable)
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
