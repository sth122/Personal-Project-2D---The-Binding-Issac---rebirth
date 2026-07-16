using UnityEngine;

public class MonsterTraceState : MonsterState
{
    public MonsterTraceState(MonsterController controller, MonsterData mData) : base(controller, mData)
    {
        this.controller = controller;
        this.mData = mData;
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
