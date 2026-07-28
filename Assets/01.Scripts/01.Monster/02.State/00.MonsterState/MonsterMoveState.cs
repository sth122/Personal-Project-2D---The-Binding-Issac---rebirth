using System;
using UnityEngine;

public class MonsterMoveState : MonsterState
{
    //bool isPattern = false;
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

    // Move만 따로 하는 애들 있을 경우 구현
    public override void Update()
    {
        if(controller.TryGetComponent<IPatternable>(out var pattern))
        {
            controller.stateMachine.ChangeState(controller.mStateDic[MonsterCurrentState.Pattern]);
        }
    }

    public override void FixedUpdate()
    {

    }
}
