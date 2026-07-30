using UnityEngine;

public class UIState : IState
{
    protected MainMenuController controller;
    protected UICurrentState state;
    public UIState(MainMenuController controller)
    {
        this.controller = controller;
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
