using System;
using UnityEngine;

public class MonsterMoveState : IState
{
    MonsterController controller;
    
    public MonsterMoveState(MonsterController controller)
    {
        this.controller = controller;
    }
    public void Enter()
    {
        controller.AnimController.TriggerMove(true);
    }

    public void Exit()
    {
        controller.AnimController.TriggerMove(false);
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }
}
