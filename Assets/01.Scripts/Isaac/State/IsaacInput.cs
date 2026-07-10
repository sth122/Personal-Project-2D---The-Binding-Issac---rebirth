using UnityEngine;

public class IsaacInput : MonoBehaviour
{
    public IsaacInputActions InputActions { get; private set; }
    public IsaacInputActions.IsaacActions IsaacActions { get; private set; }


    private void Awake()
    {
        InputActions = new IsaacInputActions();
        IsaacActions = InputActions.Isaac;
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }
    private void OnDisable()
    {
        InputActions.Disable();
    }
}
