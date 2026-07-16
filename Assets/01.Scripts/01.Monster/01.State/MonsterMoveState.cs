using System;
using UnityEngine;

public class MonsterMoveState : MonsterState
{
    
    public MonsterMoveState(MonsterController controller) : base(controller)
    {
        this.controller = controller;
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
