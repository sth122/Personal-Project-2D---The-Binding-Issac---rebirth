using System;
using UnityEngine;

public class MonsterMoveState : MonsterState
{
    
    public MonsterMoveState(MonsterController controller, MonsterData mData) : base(controller, mData)
    {
        this.controller = controller;
        this.mData = mData;
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
