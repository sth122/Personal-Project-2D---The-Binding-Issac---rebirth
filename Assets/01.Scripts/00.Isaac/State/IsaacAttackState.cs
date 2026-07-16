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
        animController.SetActiveHead(IsaacObject.Head, false);
        animController.SetActiveHead(IsaacObject.AttackHead, true);
    }

    public override void Exit()
    {
        Debug.Log("IsaacAttackState 퇴장");
        animController.SetActiveHead(IsaacObject.AttackHead, false);
        animController.SetActiveHead(IsaacObject.Head, true);
    }

    public override void Update()
    {
        attackDir = controller.Input.AttackDirection;
        dir = controller.Input.IsaacActions.Move.ReadValue<Vector2>();

        if (attackDir == Vector2.zero)
        {
            controller.stateMachine.ChangeState(controller.iStateDic[IsaacCurrentState.Move]);
            return;
        }

        animController.AttacHeadAnim(attackDir);
        animController.BodyMoveAnim(dir);
    }
}
