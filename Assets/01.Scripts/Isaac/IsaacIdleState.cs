using UnityEngine;

public class IsaacIdleState<T> : IState<T> where T : IsaacController
{
    public void Enter(T isaac)
    {
        isaac.Rb.linearVelocity = Vector2.zero;
    }

    public void Exit(T isaac)
    {

    }

    public void Update(T isaac) 
    {
        // 1. 조작키를 눌렀는지? -> 누른 조작키에 따른 행동 조정
        isaac.Rb.linearVelocity = isaac.Input.IsaacActions.Move.ReadValue<Vector2>();
        if(isaac.Rb.linearVelocity != Vector2.zero )
        {
            isaac.stateMachine.ChangeState(isaac.iMoveState);
        }

    }

    public void FixedUpdate(T isaac) { }

}
