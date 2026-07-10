using UnityEngine;

public class IsaacMoveState : BaseState<IsaacController>
{

    public override void Enter(IsaacController isaac)
    {
        // 움직이는 애니메이션 세팅
    }
    public override void Exit(IsaacController isaac) { }
    public override void Update(IsaacController isaac)
    {
        isaac.isaacDirection = isaac.Input.IsaacActions.Move.ReadValue<Vector2>();
        if(isaac.isaacDirection == Vector2.zero)
        {
            isaac.stateMachine.ChangeState(isaac.iIdleState);
        }
    }

    public override void FixedUpdate(IsaacController isaac) 
    {
        isaac.rb.linearVelocity = isaac.isaacDirection.normalized * isaac.MoveSpeed;
    }
}
