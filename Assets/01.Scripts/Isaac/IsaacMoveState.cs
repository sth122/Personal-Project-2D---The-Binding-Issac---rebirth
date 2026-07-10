using UnityEngine;

public class IsaacMoveState : IState<IsaacController>
{

    public void Enter(IsaacController isaac)
    {
        // 움직이는 애니메이션 세팅
    }
    public void Exit(IsaacController isaac) { }
    public void Update(IsaacController isaac)
    {
        isaac.isaacDirection = isaac.Input.IsaacActions.Move.ReadValue<Vector2>();
        if(isaac.isaacDirection == Vector2.zero)
        {
            isaac.stateMachine.ChangeState(isaac.iIdleState);
        }
    }

    public void FixedUpdate(IsaacController isaac) 
    {
        isaac.rb.linearVelocity = isaac.isaacDirection.normalized * isaac.MoveSpeed;
    }


}
