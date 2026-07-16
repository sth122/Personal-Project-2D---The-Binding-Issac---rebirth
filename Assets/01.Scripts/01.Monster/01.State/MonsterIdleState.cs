using UnityEngine;

public class MonsterIdleState : MonsterState
{

    public MonsterIdleState(MonsterController controller, MonsterInfo mData) 
        : base(controller, mData)
    {
        this.controller = controller;
        this.mData = mData;
    }

    public override void Enter()
    {
        Debug.Log("IdleState에 진입");
        nowState = MonsterCurrentState.Idle;
    }

    public override void Exit()
    {
        Debug.Log("IdleState에 퇴장");
    }

    public override void Update()
    {
        if(controller is ITraceable)
        {
            controller.stateMachine.ChangeState(controller.mStateDic[MonsterCurrentState.Trace]);
        }
        else
        {
            controller.stateMachine.ChangeState(controller.mStateDic[MonsterCurrentState.Move]);
        }
    }

    public override void FixedUpdate()
    {

    }
}
