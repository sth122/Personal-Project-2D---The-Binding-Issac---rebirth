using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum HeadDirection
{
    None, Left, Right, Up, Down
}

public class IsaacInput : MonoBehaviour
{
    public IsaacInputActions InputActions { get; private set; }
    public IsaacInputActions.IsaacActions IsaacActions { get; private set; }

    private readonly List<HeadDirection> preesedDirections = new();
    public HeadDirection CurrentHeadDirection { get; private set; }

    private void Awake()
    {
        InputActions = new IsaacInputActions();
        IsaacActions = InputActions.Isaac;
    }

    private void OnEnable()
    {
        InputActions.Enable();

        IsaacActions.LeftAttack.performed += OnLeftPressed;
        IsaacActions.LeftAttack.canceled += OnLeftReleased;

        IsaacActions.RigthAttack.performed += OnRightPressed;
        IsaacActions.RigthAttack.canceled += OnRightReleased;

        IsaacActions.UpAttack.performed += OnUpPressed;
        IsaacActions.UpAttack.canceled += OnUpReleased;

        IsaacActions.DownAttack.performed += OnDownPressed;
        IsaacActions.DownAttack.canceled += OnDownReleased;
    }
    private void OnDisable()
    {
        InputActions.Disable();

        IsaacActions.LeftAttack.performed -= OnLeftPressed;
        IsaacActions.LeftAttack.canceled -= OnLeftReleased;

        IsaacActions.RigthAttack.performed -= OnRightPressed;
        IsaacActions.RigthAttack.canceled -= OnRightReleased;

        IsaacActions.UpAttack.performed -= OnUpPressed;
        IsaacActions.UpAttack.canceled -= OnUpReleased;

        IsaacActions.DownAttack.performed -= OnDownPressed;
        IsaacActions.DownAttack.canceled -= OnDownReleased;
    }

    private void Press(HeadDirection dir)
    {
        preesedDirections.Remove(dir);
        preesedDirections.Add(dir);

        // 마지막 인덱스 = 가장 최근에 입력된 방향
        CurrentHeadDirection = preesedDirections[^1];
    }
    private void Release(HeadDirection dir)
    {
        preesedDirections.Remove(dir);

        if (preesedDirections.Count > 0)
        {
            CurrentHeadDirection = preesedDirections[^1];
        }
        else
        {
            CurrentHeadDirection = HeadDirection.None;
        }
    }

    #region Event
    private void OnLeftPressed(InputAction.CallbackContext context)
    {
        Press(HeadDirection.Left);
    }
    private void OnRightPressed(InputAction.CallbackContext context)
    {
        Press(HeadDirection.Right);
    }
    private void OnUpPressed(InputAction.CallbackContext context)
    {
        Press(HeadDirection.Up);
    }
    private void OnDownPressed(InputAction.CallbackContext context)
    {
        Press(HeadDirection.Down);
    }


    private void OnLeftReleased(InputAction.CallbackContext context)
    {
        Release(HeadDirection.Left);
    }
    private void OnRightReleased(InputAction.CallbackContext context)
    {
        Release(HeadDirection.Right);
    }
    private void OnUpReleased(InputAction.CallbackContext context)
    {
        Release(HeadDirection.Up);
    }
    private void OnDownReleased(InputAction.CallbackContext context)
    {
        Release(HeadDirection.Down);
    }
    #endregion

    public Vector2 AttackDirection
    {
        get
        {
            return CurrentHeadDirection switch
            {
                HeadDirection.Left => Vector2.left,
                HeadDirection.Right => Vector2.right,
                HeadDirection.Up => Vector2.up,
                HeadDirection.Down => Vector2.down,
                _ => Vector2.zero
            };
        }
    }
}
