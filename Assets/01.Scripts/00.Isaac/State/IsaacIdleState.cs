using UnityEngine;

public class IsaacIdleState : IState
{
    IsaacController controller;
    public IsaacIdleState(IsaacController controller)
    {
        this.controller = controller;
    }

    Vector2 dir;
    public void Enter()
    {
        controller.RB.linearVelocity = Vector2.zero;
    }

    public void Exit()
    {

    }

    public void Update()
    {
        // 1. 조작키를 눌렀는지? -> 누른 조작키에 따른 행동 조정
        dir = controller.Input.IsaacActions.Move.ReadValue<Vector2>();
        if (dir != Vector2.zero)
        {
            controller.stateMachine.ChangeState(controller.iMoveState);
        }
    }

    public void FixedUpdate()
    {

    }
}
