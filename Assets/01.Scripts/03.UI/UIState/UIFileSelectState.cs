
using UnityEngine;

public class UIFileSelectState : MainMenuState
{
    public UIFileSelectState(MainMenuController controller) : base(controller)
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        controller.targetPositionY = controller.pageHeight;

        controller.currentFileIdx = 0;
        controller.UpdateFileSlotUI();
    }


    public override void OnSubmit()
    {
        controller.stateMachine.ChangeState(controller.uiStateDic[UICurrentState.GameMenu]);
    }

    public override void OnCancel()
    {
        controller.stateMachine.ChangeState(controller.uiStateDic[UICurrentState.Title]);
    }

    public override void OnNavigate(Vector2 dir)
    {
        if (dir.x >= 0.5f)
        {
            controller.currentFileIdx = (controller.currentFileIdx + 1) % controller.fileSlotsCount;
            controller.UpdateFileSlotUI();
        }
        else if (dir.x < -0.5f)
        {
            controller.currentFileIdx = (controller.currentFileIdx - 1 + controller.fileSlotsCount) % controller.fileSlotsCount;
            controller.UpdateFileSlotUI();
        }
    }
}