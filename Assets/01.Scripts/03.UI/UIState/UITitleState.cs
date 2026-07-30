using UnityEngine;
public class UITitleState : MainMenuState
{
    public UITitleState(MainMenuController controller) : base(controller)
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        controller.targetPositionY = 0f;
    }

    public override void OnSubmit()
    {
        controller.stateMachine.ChangeState(controller.uiStateDic[UICurrentState.FileSelect]);
    }

    public override void OnCancel()
    {
        controller.QuitGame();
    }
}
