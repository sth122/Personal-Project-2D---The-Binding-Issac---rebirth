using UnityEngine;

public class MonsterTraceState : MonsterState
{
    public MonsterTraceState(MonsterController controller, MonsterData mData, MonsterCurrentState nowState) : base(controller, mData, nowState)
    {
        this.controller = controller;
        this.mData = mData;
        this.nowState = nowState;
    }


    public override void Enter()
    {
        Debug.Log("TraceState에 진입");
        controller.AnimController.TriggerMove(true);
    }

    public override void Exit()
    {
        Debug.Log("TraceState에 퇴장");
        controller.AnimController.TriggerMove(false);
    }

    public override void Update()
    {
        if (controller is ITraceable traceMonster)
        {
            traceMonster.Trace(); 
        }
    }
}
