using UnityEngine;

public class UIGameMenuState : UIState
{
    public UIGameMenuState(MainMenuController controller) : base(controller)
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        state = UICurrentState.GameMenu;
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
