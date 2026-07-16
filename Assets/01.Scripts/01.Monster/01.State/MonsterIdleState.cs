using UnityEngine;

public class MonsterIdleState : MonsterState
{

    public MonsterIdleState(MonsterController controller) : base(controller)
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        Debug.Log("IdleState에 진입");
        // Appear Anim 실행
        // Anim 끝날 때 까지 대기 후 실행
    }

    public override void Exit()
    {
        Debug.Log("IdleState에 퇴장");
    }

    public override void Update()
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

    public override void FixedUpdate()
    {

    }
}
