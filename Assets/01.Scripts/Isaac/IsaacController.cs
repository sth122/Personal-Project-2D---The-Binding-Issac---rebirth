using UnityEngine;

public class IsaacController : MonoBehaviour
{
    public StateMachine<IsaacController> stateMachine;
    public IsaacIdleState<IsaacController> iIdleState;
    public IsaacMoveState<IsaacController> iMoveState;
    public IsaacAttackState<IsaacController> iAttackState;
    
    void Start()
    {
        stateMachine = new StateMachine<IsaacController>();
        iIdleState = new IsaacIdleState<IsaacController>();
        iMoveState = new IsaacMoveState<IsaacController>();
        iAttackState = new IsaacAttackState<IsaacController>();

        stateMachine.ChangeState(iIdleState);
    }

    // Update is called once per frame
    void Update()
    {
        //stateMachine.Update();
    }
}
