using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAppearState : MonsterState
{
    public MonsterAppearState(MonsterController controller) : base(controller)
    {
        this.controller = controller;
    }


    public override void Enter()
    {
        Debug.Log("MonsterAppearState 입장");
    }

    public  override void Exit()
    {
        Debug.Log("MonsterAppearState 퇴장");
    }

    public override void Update()
    {
        if (controller.AnimController.IsAnimationFinished())
        {
            controller.stateMachine.ChangeState(controller.mIdleState);
        }
    }

    public override void FixedUpdate()
    { }
}
