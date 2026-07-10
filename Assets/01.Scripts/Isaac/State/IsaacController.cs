using UnityEngine;

public class IsaacController : MonoBehaviour
{
    #region MyRegion
    [field: Header("Animations")]
    [field: SerializeField] public IsaacAinmData AnimData { get; private set; }

    [SerializeField] GameObject head;
    [SerializeField] GameObject body;
    public StateMachine<IsaacController> stateMachine;
    public IsaacIdleState iIdleState;
    public IsaacMoveState iMoveState;
    public Rigidbody2D rb;
    public Animator HeadAnimator { get; private set; }
    public Animator BodyAnimator { get; private set; }
    public IsaacInput Input { get; private set; }
    public Vector2 isaacDirection { get; set; }
    private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }
    #endregion

    private void Awake()
    {
        AnimData.Initialize();

        stateMachine = new StateMachine<IsaacController>(this);
        iIdleState = new IsaacIdleState();
        iMoveState = new IsaacMoveState();
        HeadAnimator = head.GetComponent<Animator>();
        BodyAnimator = body.GetComponent<Animator>();
        Input = GetComponent<IsaacInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        MoveSpeed = 3f;
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
