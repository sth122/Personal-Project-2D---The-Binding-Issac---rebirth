using UnityEngine;

public class IsaacMoveState : IState
{
    IsaacController controller;
    SpriteRenderer sr;
    Vector2 dir;
    int moveHash;
    int upDownHash;

    public IsaacMoveState(IsaacController controller)
    {
        this.controller = controller;
    }


    public void Enter()
    {
        sr = controller.body.GetComponent<SpriteRenderer>();
        moveHash = controller.IsBodyMove;
        upDownHash = controller.IsBodyUpDown;
        // 움직이는 애니메이션 세팅
        
    }
    public void Exit() 
    {

    }
    public void Update()
    {
        dir = controller.Input.IsaacActions.Move.ReadValue<Vector2>();

        AnimationUpdate();

        if (controller.RB.linearVelocity == Vector2.zero)
        {
            controller.stateMachine.ChangeState(controller.iIdleState);
            return;
        }
    }

    public void FixedUpdate() 
    {
        controller.RB.linearVelocity = dir.normalized * controller.MoveSpeed;
    }

    private void AnimationUpdate()
    {
        if(dir.y != 0)
        {
            controller.BodyAnimator.SetBool(upDownHash, true);
            controller.BodyAnimator.SetBool(moveHash, false);
            return;
        }
        else
        {
            controller.BodyAnimator.SetBool(upDownHash, false);
        }
        
        if (dir.x != 0)
        {
            controller.BodyAnimator.SetBool(moveHash, true);
            if (dir.x > 0)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }
        else
        {
            controller.BodyAnimator.SetBool(moveHash, false);
        }
    }
}
