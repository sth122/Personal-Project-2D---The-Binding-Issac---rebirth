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

    }

    public void Exit()
    {

    }

    public void Update()
    {
        if (controller is ITraceable traceMonster)
            traceMonster.Trace();
    }

    public void FixedUpdate()
    {

    }
}
