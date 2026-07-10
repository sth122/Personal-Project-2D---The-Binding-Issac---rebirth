using UnityEngine;
using UnityEngine.InputSystem;

public class IsaacMoveState<T> : IState<T> where T : IsaacController
{

    public void Enter(T isaac)
    {
        // 움직이는 애니메이션 세팅
    }
    public void Exit(T isaac) { }
    public void Update(T isaac)
    {
        isaac.isaacDirection = isaac.Input.IsaacActions.Move.ReadValue<Vector2>();
        if(isaac.isaacDirection == Vector2.zero)
        {
            isaac.stateMachine.ChangeState(isaac.iIdleState);
        }
    }

    public void FixedUpdate(T isaac) 
    {
        isaac.Rb.linearVelocity = isaac.isaacDirection.normalized * isaac.MoveSpeed;
    }

    private void OnMove(InputAction.CallbackContext context)
    {

    }

    private void Move()
    {

    }
}
