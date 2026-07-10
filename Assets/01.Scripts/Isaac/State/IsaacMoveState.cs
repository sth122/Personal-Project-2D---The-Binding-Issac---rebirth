using UnityEngine;

public class IsaacMoveState : IState<IsaacController>
{
    SpriteRenderer sr;
    Vector2 dir;
    //int moveHash;
    //int upDownHash;

    public void Enter(IsaacController isaac)
    {

        //moveHash = isaac.IsBodyMove;
        //upDownHash = isaac.IsBodyUpDown;

        sr = isaac.body.GetComponent<SpriteRenderer>();
        // 움직이는 애니메이션 세팅
        
    }
    public void Exit(IsaacController isaac) 
    {

    }
    public void Update(IsaacController isaac)
    {
        //isaac.isaacDirection = isaac.Input.IsaacActions.Move.ReadValue<Vector2>();
        dir = isaac.Input.IsaacActions.Move.ReadValue<Vector2>();

        AnimationUpdate(isaac);

        if (dir == Vector2.zero)
        {
            isaac.stateMachine.ChangeState(isaac.iIdleState);
            return;
        }
    }

    public void FixedUpdate(IsaacController isaac) 
    {
        isaac.rb.linearVelocity = dir.normalized * isaac.MoveSpeed;
    }

    private void AnimationUpdate(IsaacController isaac)
    {
        if(dir.y != 0)
        {
            isaac.BodyAnimator.SetBool(isaac.IsBodyUpDown, true);
            isaac.BodyAnimator.SetBool(isaac.IsBodyMove, false);
            return;
        }
        else
        {
            isaac.BodyAnimator.SetBool(isaac.IsBodyUpDown, false);
        }
        
        if (dir.x != 0)
        {
            isaac.BodyAnimator.SetBool(isaac.IsBodyMove, true);
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
            isaac.BodyAnimator.SetBool(isaac.IsBodyMove, false);
        }
    }
}
