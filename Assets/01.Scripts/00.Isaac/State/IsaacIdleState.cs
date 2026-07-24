using UnityEngine;

public class IsaacIdleState : IsaacState
{
    public IsaacIdleState(IsaacController controller, IsaacAnimController animController, Rigidbody2D rb, IsaacInfo isaacInfo) : base(controller, animController, rb, isaacInfo)
    {
        this.controller = controller;
        this.animController = animController;
        this.rb = rb;
        this.isaacInfo = isaacInfo;
    }

    public override void Enter()
    {
        nowState = IsaacCurrentState.Idle;

        rb.linearVelocity = Vector2.zero;
    }

    public override void Update()
    {
        // 1. 조작키를 눌렀는지?
        // 조작키를 눌렀을 시 state -> MoveState
        // 공격키를 눌렀을 시 state -> AttackState
        attackDir = controller.Input.AttackDirection;
        moveDir = controller.Input.IsaacActions.Move.ReadValue<Vector2>();

        if (attackDir != Vector2.zero)
        {
            controller.stateMachine.ChangeState(controller.iStateDic[IsaacCurrentState.Attack]);
            return;
        }

        if (moveDir != Vector2.zero)
        {
            controller.stateMachine.ChangeState(controller.iStateDic[IsaacCurrentState.Move]);
            return;
        }
    }
}
