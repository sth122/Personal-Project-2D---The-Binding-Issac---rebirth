using UnityEngine;

public class IsaacDieState : IsaacState
{
    private Collider2D[] col;

    public IsaacDieState(IsaacController controller, IsaacAnimController animController, Rigidbody2D rb, IsaacInfo isaacInfo) : base(controller, animController, rb, isaacInfo)
    {
        this.controller = controller;
        this.animController = animController;
        this.rb = rb;
        this.isaacInfo = isaacInfo;
    }

    public override void Enter()
    {
        rb.linearVelocity = Vector3.zero;
        animController.SetAnimTrigger(IsaacAnimState.Die, true);
        // 게임 시작할 때 다시 true 변경 필요
        rb.bodyType = RigidbodyType2D.Kinematic;
        col = controller.GetComponentsInChildren<Collider2D>();
        foreach(var c in col)
        {
            c.enabled = false;
        }
        // Die 애니메이션 종료 후 UI 출력
        IsaacManager.Instance.IsaacDie();
    }

    public override void FixedUpdate()
    {
        rb.linearVelocity = Vector3.zero;
    }

    public override void Exit()
    {

    }
}
