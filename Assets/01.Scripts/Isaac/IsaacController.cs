using UnityEngine;

public class IsaacController : MonoBehaviour
{
    public StateMachine<IsaacController> stateMachine;
    public IsaacIdleState<IsaacController> iIdleState;
    public IsaacMoveState<IsaacController> iMoveState;

    
    void Start()
    {
        stateMachine = new StateMachine<IsaacController>();
        iIdleState = new IsaacIdleState<IsaacController>();
        iMoveState = new IsaacMoveState<IsaacController>();

        stateMachine.ChangeState(iIdleState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
