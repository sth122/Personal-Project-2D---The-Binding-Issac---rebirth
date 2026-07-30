using UnityEngine;

abstract public class PatternState : MonsterState
{
    public PatternState(MonsterController controller, MonsterInfo mData) : base(controller, mData)
    {
        this.controller = controller;
        this.mData = mData;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
