using UnityEngine;

abstract public class MonsterController : MonoBehaviour
{
	#region variable
	public StateMachine<MonsterController> stateMachine;
	public MonsterIdleState mIdleState;
	public MonsterMoveState mMoveState;
	public MonsterTraceState mTraceState;
    protected Monster mStat;

    #endregion

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        stateMachine.Update(this);
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.FixedUpdate(this);
    }

    public void InitData(Monster data)
    {
        mStat = data.Clone();
        OnDataLodead();
    }

    protected virtual void OnDataLodead() { }
}
