using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum AttackInputDirection
{
    None, Left, Right, Up, Down
}

public class IsaacInput : MonoBehaviour
{
    public IsaacInputActions InputActions { get; private set; }
    public IsaacInputActions.IsaacActions IsaacActions { get; private set; }

    // 현재 누르고 있는 모든 키들의 목록
    private readonly List<AttackInputDirection> preesedDirections = new();
    public AttackInputDirection CurrentHeadDirection
    {
        get
        {
            if (preesedDirections.Count == 0) return AttackInputDirection.None;


            var verticalDir = preesedDirections.FindLast(dir => dir == AttackInputDirection.Up || dir == AttackInputDirection.Down);

            if (verticalDir != AttackInputDirection.None)
            {
                return verticalDir; 
            }

            return preesedDirections[^1];
        }
    }

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

    #region Event
    private void OnLeftPressed(InputAction.CallbackContext context)
    {
        Press(AttackInputDirection.Left);
    }
    private void OnRightPressed(InputAction.CallbackContext context)
    {
        Press(AttackInputDirection.Right);
    }
    private void OnUpPressed(InputAction.CallbackContext context)
    {
        Press(AttackInputDirection.Up);
    }
    private void OnDownPressed(InputAction.CallbackContext context)
    {
        Press(AttackInputDirection.Down);
    }


    private void OnLeftReleased(InputAction.CallbackContext context)
    {
        Release(AttackInputDirection.Left);
    }
    private void OnRightReleased(InputAction.CallbackContext context)
    {
        Release(AttackInputDirection.Right);
    }
    private void OnUpReleased(InputAction.CallbackContext context)
    {
        Release(AttackInputDirection.Up);
    }
    private void OnDownReleased(InputAction.CallbackContext context)
    {
        Release(AttackInputDirection.Down);
    }
    #endregion


    private void Press(AttackInputDirection dir)
    {
        preesedDirections.Remove(dir);
        preesedDirections.Add(dir);
    }
    private void Release(AttackInputDirection dir)
    {
        preesedDirections.Remove(dir);
    }

    /// <summary>
    /// 현재 공격 입력 방향 반환
    /// </summary>
    public Vector2 AttackDirection
    {
        get
        {
            return CurrentHeadDirection switch
            {
                AttackInputDirection.Left => Vector2.left,
                AttackInputDirection.Right => Vector2.right,
                AttackInputDirection.Up => Vector2.up,
                AttackInputDirection.Down => Vector2.down,
                _ => Vector2.zero
            };
        }
    }
}
