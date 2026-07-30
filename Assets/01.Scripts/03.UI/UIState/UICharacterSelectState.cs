using UnityEngine;
using UnityEngine.SceneManagement;

public class UICharacterSelectState : MainMenuState
{
    public UICharacterSelectState(MainMenuController controller) : base(controller)
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        controller.targetPositionY = controller.pageHeight * 3;
    }

    public override void OnSubmit()
    {
        SceneManager.LoadScene("GameScene");
    }
    public override void OnCancel()
    {
        controller.ChangeUIState(UICurrentState.GameMenu);
    }

}
