using System;
using UnityEngine;

public class MonsterMoveState : MonsterState
{
    
    public MonsterMoveState(MonsterController controller, MonsterInfo mData) : base(controller, mData)
    {
        this.controller = controller;
        this.mData = mData;
    }
    public override void Enter()
    {
        Debug.Log("Move에 진입");
        nowState = MonsterCurrentState.Move;
        controller.AnimController.AnimationStart(nowState);
    }

    public override void Exit()
    {
        Debug.Log("Move에서 퇴장");
        controller.AnimController.AnimationStop(nowState);
    }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {

    }
}
