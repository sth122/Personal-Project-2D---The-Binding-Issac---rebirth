using UnityEngine;

public class MainMenuState : IState
{
    protected MainMenuController controller;
    public MainMenuState(MainMenuController controller)
    {
        this.controller = controller;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }

    public virtual void OnSubmit() { }
    public virtual void OnCancel() { }
    public virtual void OnNavigate(Vector2 dir) { }
}
