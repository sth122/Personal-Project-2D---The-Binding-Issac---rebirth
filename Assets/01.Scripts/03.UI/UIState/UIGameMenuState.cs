using UnityEngine;

public class UIGameMenuState : MainMenuState
{
    public UIGameMenuState(MainMenuController controller) : base(controller)
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        controller.targetPositionY = controller.pageHeight * 2;

        controller.currentGameMenuIdx = 0;
        controller.UpdateCursorPosition();

    }

    public override void OnSubmit()
    {
        controller.ExecuteGameMenuAction();
    }


    public override void OnCancel()
    {
        controller.ChangeUIState(UICurrentState.FileSelect);
    }
    public override void OnNavigate(Vector2 dir)
    {
        if (dir.y > 0.5f) // 위로 이동 (인덱스 감소)
        {
            controller.currentGameMenuIdx = (controller.currentGameMenuIdx - 1 + controller.gameMenuItemsCount) % controller.gameMenuItemsCount;
            controller.UpdateCursorPosition();
        }
        else if (dir.y < -0.5f) // 아래로 이동 (인덱스 증가)
        {
            controller.currentGameMenuIdx = (controller.currentGameMenuIdx + 1) % controller.gameMenuItemsCount;
            controller.UpdateCursorPosition();
        }
        SoundManager.Instance.PlayScrollSFX();
    }

}

