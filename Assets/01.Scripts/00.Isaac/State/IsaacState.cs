using UnityEngine;

public class IsaacState : IState
{
    protected IsaacController controller;
    protected IsaacAnimController animController;
    protected Rigidbody2D rb;
    protected IsaacInfo isaacInfo;
    protected IsaacCurrentState nowState;
    protected Vector2 attackDir;
    protected Vector2 dir;
    public IsaacState(IsaacController controller, IsaacAnimController animController, Rigidbody2D rb, IsaacInfo isaacInfo)
    {
        this.controller = controller;
        this.animController = animController;
        this.rb = rb;
        this.isaacInfo = isaacInfo;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }
}
