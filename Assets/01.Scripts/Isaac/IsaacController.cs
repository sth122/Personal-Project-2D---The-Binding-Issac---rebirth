using UnityEngine;

public class IsaacController : MonoBehaviour
{
    public StateMachine<IsaacController> stateMachine;
    public IsaacIdleState<IsaacController> iIdleState;
    public IsaacMoveState<IsaacController> iMoveState;
    public IsaacAttackState<IsaacController> iAttackState;
    public IsaacInput Input { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public Vector2 isaacDirection { get; set; }
    private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    private void Awake()
    {
        stateMachine = new StateMachine<IsaacController>();
        iIdleState = new IsaacIdleState<IsaacController>();
        iMoveState = new IsaacMoveState<IsaacController>();
        iAttackState = new IsaacAttackState<IsaacController>();
        Input = GetComponent<IsaacInput>();
        Rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        MoveSpeed = 1f;
        stateMachine.ChangeState(iIdleState);
    }

    // Update is called once per frame
    private void Update()
    {
        stateMachine.Update(this);
    }

    private void FixedUpdate()
    {
        stateMachine.Update(this);
    }
}
