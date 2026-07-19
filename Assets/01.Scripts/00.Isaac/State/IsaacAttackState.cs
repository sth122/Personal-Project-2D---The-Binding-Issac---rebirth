using UnityEngine;

public class IsaacAttackState : IsaacState
{
    public IsaacAttackState(IsaacController controller, IsaacAnimController animController, Rigidbody2D rb, IsaacInfo isaacInfo) : base(controller, animController, rb, isaacInfo)
    {
        this.controller = controller;
        this.animController = animController;
        this.rb = rb;
        this.isaacInfo = isaacInfo;
    }

    public override void Enter()
    {
        Debug.Log("IsaacAttackState 진입");
        animController.SetAnimTrigger(IsaacAnimState.Attack ,true);
    }

    public override void Exit()
    {
        Debug.Log("IsaacAttackState 퇴장");
        animController.SetFalseAttackAnim();
        animController.SetAnimTrigger(IsaacAnimState.Attack, false);
    }

    public override void Update()
    {
        attackDir = controller.Input.AttackDirection;
        //attackDir = controller.Input.IsaacActions.Attack.ReadValue<Vector2>();
        moveDir = controller.Input.IsaacActions.Move.ReadValue<Vector2>();

        animController.BaseMoveAnim(moveDir);
        animController.HeadMoveAnim(attackDir);

        // 1. 공격키를 눌렀는지
        // 2. 공격키를 누르지 않았을 경우
        // 2-1 이동키를 눌렀을 경우 AttackState -> MoveState
        // 2-1 이동키를 누르지 않을 경우 AttackState -> IdleState
        if (attackDir == Vector2.zero)
        {
            if(moveDir !=  Vector2.zero)
            {
                controller.stateMachine.ChangeState(controller.iStateDic[IsaacCurrentState.Move]);
            }
            else
            {
                controller.stateMachine.ChangeState(controller.iStateDic[IsaacCurrentState.Idle]);
            }
            return;
        }
    }

    public override void FixedUpdate()
    {
        rb.linearVelocity = moveDir.normalized * isaacInfo.speed;
    }
}
