using UnityEngine;

public class IsaacController : MonoBehaviour
{
    #region variable
    [SerializeField] public GameObject head;

    [SerializeField] public GameObject body;

    public StateMachine<IsaacController> stateMachine;
    public IsaacIdleState iIdleState;
    public IsaacMoveState iMoveState;
    private Rigidbody2D rb;
    public Rigidbody2D RB {  get { return rb; } }
    public Animator BodyAnimator { get; private set; }
    public IsaacInput Input { get; private set; }
    private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }
    #endregion

    #region body animation variable
    public int IsBodyMove { get; private set; }
    public int IsBodyUpDown { get; private set; }
    #endregion

    private void Awake()
    {
        AnimationInitialize();

        stateMachine = new StateMachine<IsaacController>(this);
        iIdleState = new IsaacIdleState(this);
        iMoveState = new IsaacMoveState(this);
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
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    private void AnimationInitialize()
    {
        IsBodyMove = Animator.StringToHash("isBodyMove");
        IsBodyUpDown = Animator.StringToHash("isBodyUpDown");
    }
}
