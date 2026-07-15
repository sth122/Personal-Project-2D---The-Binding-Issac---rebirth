using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAppearState : IState
{
    MonsterController controller;
    public MonsterAppearState(MonsterController controller)
    {
        this.controller = controller;
    }


    public void Enter()
    {
        Debug.Log("MonsterAppearState 입장");
    }

    public void Exit()
    {
        Debug.Log("MonsterAppearState 퇴장");
    }

    public void Update()
    {
        if (controller.AnimController.IsAnimationFinished())
        {
            controller.stateMachine.ChangeState(controller.mIdleState);
        }
    }

    public void FixedUpdate()
    { }
}
