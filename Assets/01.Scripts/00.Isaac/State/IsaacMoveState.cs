using UnityEngine;

public class IsaacMoveState : IsaacState
{ 
    public IsaacMoveState(IsaacController controller, IsaacAnimController animController, Rigidbody2D rb, IsaacInfo isaacInfo) : base(controller, animController, rb, isaacInfo)
    {
        this.controller = controller;
        this.animController = animController;
        this.rb = rb;
        this.isaacInfo = isaacInfo;
    }

    public override void Enter()
    {
        Debug.Log("IsaacMoveState 진입");
    }
    public override void Exit() 
    {
        Debug.Log("IsaacMoveState 퇴장");
    }
    public override void Update()
    {
        attackDir = controller.Input.AttackDirection;
        moveDir = controller.Input.IsaacActions.Move.ReadValue<Vector2>();
        animController.BaseMoveAnim(moveDir);

        if (rb.linearVelocity == Vector2.zero)
        {
            controller.stateMachine.ChangeState(controller.iStateDic[IsaacCurrentState.Idle]);
            return;
        }

        if(attackDir != Vector2.zero)
        {
            controller.stateMachine.ChangeState(controller.iStateDic[IsaacCurrentState.Attack]);
            return;
        }
    }

    public override void FixedUpdate() 
    {
        rb.linearVelocity = moveDir.normalized * isaacInfo.speed;
    }
}
