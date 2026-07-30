using UnityEngine;

public class UIFileSelectState : UIState
{
    public UIFileSelectState(MainMenuController controller) : base(controller)
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        state = UICurrentState.FileSelect;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {

    }
    public override void FixedUpdate()
    {

    }
}