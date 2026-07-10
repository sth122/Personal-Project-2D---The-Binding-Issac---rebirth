using UnityEngine;

public class IsaacIdleState : IState<IsaacController>
{
    //IsaacController? isaac = null;

    public void Enter(IsaacController obj)
    {
        //if (obj != null)
        //{
        //isaac = obj as IsaacController;
        obj.rb.linearVelocity = Vector2.zero;
        //}
        //else { Debug.Log("Null error"); }
    }

    public void Exit(IsaacController obj)
    {

    }

    public void Update(IsaacController obj) 
    {
        // 1. 조작키를 눌렀는지? -> 누른 조작키에 따른 행동 조정
        obj.isaacDirection = obj.Input.IsaacActions.Move.ReadValue<Vector2>();
        if(obj.isaacDirection != Vector2.zero )
        {
            obj.stateMachine.ChangeState(obj.iMoveState);
        }
    }

    public void FixedUpdate(IsaacController obj) 
    {

    }

}
