using System;
using UnityEngine;

public class MonsterMoveState : MonsterState
{
    
    public MonsterMoveState(MonsterController controller, MonsterData mData, MonsterCurrentState nowState) : base(controller, mData, nowState)
    {
        this.controller = controller;
        this.mData = mData;
        this.nowState = nowState;
    }
    public override void Enter()
    {
        controller.AnimController.TriggerMove(true);
    }

    public override void Exit()
    {
        controller.AnimController.TriggerMove(false);
    }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {

    }
}
