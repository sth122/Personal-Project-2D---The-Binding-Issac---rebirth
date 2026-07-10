using UnityEngine;

public class IsaacMoveState : IState<IsaacController>
{
    SpriteRenderer sr;
    Vector2 dir;
    int moveHash;
    int upDownHash;
    bool isMovingX;
    bool isMovingY;

    public void Enter(IsaacController isaac)
    {

        moveHash = isaac.AnimData.MoveParameterHash;
        upDownHash = isaac.AnimData.UpDownParameterHash;

        sr = isaac.body.GetComponent<SpriteRenderer>();
        // 움직이는 애니메이션 세팅
        
    }
    public void Exit(IsaacController isaac) 
    {
        isaac.BodyAnimator.SetBool(moveHash, false);
    }
    public void Update(IsaacController isaac)
    {
        //isaac.isaacDirection = isaac.Input.IsaacActions.Move.ReadValue<Vector2>();
        dir = isaac.Input.IsaacActions.Move.ReadValue<Vector2>();
        if(dir == Vector2.zero)
        {
            isaac.stateMachine.ChangeState(isaac.iIdleState);
        }
    }

    public void FixedUpdate(IsaacController isaac) 
    {
        AnimationUpdate(isaac);

        isaac.rb.linearVelocity = dir.normalized * isaac.MoveSpeed;
    }

    private void AnimationUpdate(IsaacController isaac)
    {
        isMovingX = dir.x != 0;
        isMovingY = dir.x == 0 && dir.y != 0;

        isaac.BodyAnimator.SetBool(moveHash, isMovingX);
        isaac.BodyAnimator.SetBool(upDownHash, isMovingY);

        if (isMovingX)
        {
            if (dir.x > 0)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }
    }
}
