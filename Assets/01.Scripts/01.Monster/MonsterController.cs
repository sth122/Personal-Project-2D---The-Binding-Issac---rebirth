using UnityEngine;

abstract public class MonsterController : MonoBehaviour
{
	#region variable
	public StateMachine<MonsterController> stateMachine;
	public MonsterIdleState mIdleState;
	public MonsterMoveState mMoveState;
	public MonsterTraceState mTraceState;
	#endregion
}
