using UnityEngine;

public class IsaacController : MonoBehaviour
{
    public StateMachine<IsaacController> stateMachine;
    public IsaacIdleState iIdleState;
    public IsaacMoveState iMoveState;
    public IsaacAttackState iAttackState;
    public IsaacInput Input { get; private set; }
    public Rigidbody2D rb;
    public Vector2 isaacDirection { get; set; }
    private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    private void Awake()
    {
        stateMachine = new StateMachine<IsaacController>(this);
        iIdleState = new IsaacIdleState();
        iMoveState = new IsaacMoveState();
        iAttackState = new IsaacAttackState();
        Input = GetComponent<IsaacInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        MoveSpeed = 10f;
        stateMachine?.ChangeState(iIdleState);
    }

    // Update is called once per frame
    private void Update()
    {
        stateMachine.Update(this);
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate(this);
    }
}
