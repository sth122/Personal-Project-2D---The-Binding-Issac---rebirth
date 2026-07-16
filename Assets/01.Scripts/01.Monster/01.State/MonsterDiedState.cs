using UnityEngine;

public class MonsterDiedState : MonsterState
{

    public MonsterDiedState(MonsterController controller, MonsterData mData, MonsterCurrentState nowState) : base(controller, mData, nowState)
    {
        this.controller = controller;
        this.mData = mData;
        this.nowState = nowState;
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {

    }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {

    }
}
