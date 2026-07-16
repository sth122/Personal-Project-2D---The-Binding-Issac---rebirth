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

        IsaacActions.LeftAttack.performed += _ => Press(AttackInputDirection.Left);
        IsaacActions.LeftAttack.canceled += _ => Release(AttackInputDirection.Left);

        IsaacActions.RigthAttack.performed += _ => Press(AttackInputDirection.Right);
        IsaacActions.RigthAttack.canceled += _ => Release(AttackInputDirection.Right);

        IsaacActions.UpAttack.performed += _ => Press(AttackInputDirection.Up);
        IsaacActions.UpAttack.canceled += _ => Release(AttackInputDirection.Up);

        IsaacActions.DownAttack.performed += _ => Press(AttackInputDirection.Down);
        IsaacActions.DownAttack.canceled += _ => Release(AttackInputDirection.Down);
    }
    private void OnDisable()
    {
        InputActions.Disable();

        IsaacActions.LeftAttack.performed -= _ => Press(AttackInputDirection.Left);
        IsaacActions.LeftAttack.canceled -= _ => Release(AttackInputDirection.Left);

        IsaacActions.RigthAttack.performed -= _ => Press(AttackInputDirection.Right);
        IsaacActions.RigthAttack.canceled -= _ => Release(AttackInputDirection.Right);

        IsaacActions.UpAttack.performed -= _ => Press(AttackInputDirection.Up);
        IsaacActions.UpAttack.canceled -= _ => Release(AttackInputDirection.Up);

        IsaacActions.DownAttack.performed -= _ => Press(AttackInputDirection.Down);
        IsaacActions.DownAttack.canceled -= _ => Release(AttackInputDirection.Down);
    }

    private void Press(AttackInputDirection dir)
    {
        preesedDirections.Remove(dir);
        preesedDirections.Add(dir);
    }
    private void Release(AttackInputDirection dir)
    {
        preesedDirections.Remove(dir);
    }

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
