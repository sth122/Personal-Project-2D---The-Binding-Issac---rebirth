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
        nowState = MonsterCurrentState.Move;
        controller.AnimController.AnimationStart(nowState);
    }

    public override void Exit()
    {
        Debug.Log("TraceState에서 퇴장");
        controller.AnimController.AnimationStop(nowState);
    }

    public override void Update()
    {
        if (controller is ITraceable traceMonster)
        {
            traceMonster.Trace(); 
        }
    }
}
