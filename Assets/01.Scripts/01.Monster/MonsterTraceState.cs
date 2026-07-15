using UnityEngine;

public class MonsterTraceState : IState
{
    MonsterController controller;

    public MonsterTraceState(MonsterController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        Debug.Log("TraceState에 진입");
    }

    public void Exit()
    {
        Debug.Log("TraceState에 퇴장");
    }

    public void Update()
    {
        if (controller is ITraceable traceMonster)
        {
            traceMonster.Trace(); 
        }
    }

    public void FixedUpdate()
    {

    }
}
