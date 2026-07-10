using UnityEngine;

public class IsaacIdleState : IState<IsaacController>
{
    public void Enter(IsaacController isaac)
    {
        isaac.rb.linearVelocity = Vector2.zero;
    }

    public void Exit(IsaacController isaac)
    {

    }

    public void Update(IsaacController isaac)
    {
        // 1. 조작키를 눌렀는지? -> 누른 조작키에 따른 행동 조정
        isaac.isaacDirection = isaac.Input.IsaacActions.Move.ReadValue<Vector2>();
        if (isaac.isaacDirection != Vector2.zero)
        {
            isaac.stateMachine.ChangeState(isaac.iMoveState);
        }
    }

    public void FixedUpdate(IsaacController isaac)
    {

    }

}
