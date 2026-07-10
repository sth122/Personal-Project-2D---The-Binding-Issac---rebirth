using UnityEngine;
using UnityEngine.InputSystem;

public class IsaacMoveState<T> : IState<T>
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private Vector2 curMovementInput;
    Rigidbody2D rb;

    private void Awake()
    {

    }

    public void Enter(T obj)
    {
        
    }
    public void Exit(T obj) { }
    public void Update(T obj)
    {
        
    }

    public void FixedUpdate(T obj) 
    {
        Move();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Performed)
        {
            curMovementInput = Vector2.zero;
        }
    }

    private void Move()
    {

    }
}
