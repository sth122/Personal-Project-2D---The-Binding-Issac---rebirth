using UnityEngine;

public class IsaacIdleState : CharacterState<IsaacController>
{
    public override void Enter(IsaacController isaac)
    {
        isaac.rb.linearVelocity = Vector2.zero;
    }

    public override void Exit(IsaacController isaac)
    {

    }

    public override void Update(IsaacController isaac)
    {
        // 1. 조작키를 눌렀는지? -> 누른 조작키에 따른 행동 조정
        isaac.isaacDirection = isaac.Input.IsaacActions.Move.ReadValue<Vector2>();
        if (isaac.isaacDirection != Vector2.zero)
        {
            isaac.stateMachine.ChangeState(isaac.iMoveState);
        }
    }

    public override void FixedUpdate(IsaacController isaac)
    {

    }

}
