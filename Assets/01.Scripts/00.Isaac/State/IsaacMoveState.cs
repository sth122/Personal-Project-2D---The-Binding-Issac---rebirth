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
        animController.SetBoolAllAnim(IsaacCurrentState.Move);
    }
    public override void Update()
    {
        attackDir = controller.Input.AttackDirection;
        moveDir = controller.Input.IsaacActions.Move.ReadValue<Vector2>();

        animController.BaseMoveAnim(moveDir);


        // 1. 조작키를 눌렀는지?
        // 조작키를 눌렀을 시 MoveState Update
        // Isaac의 움직임(물리력)이 없을 시 MoveState -> IdleState
        // 공격키를 눌렀을 시 MoveState -> AttackState

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
