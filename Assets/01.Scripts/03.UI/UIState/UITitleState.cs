using UnityEngine;
public class UITitleState : UIState
{
    public UITitleState(MainMenuController controller) : base(controller)
    {
        this.controller = controller;
    }

    public override void Enter() 
    {
        state = UICurrentState.Title;
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
