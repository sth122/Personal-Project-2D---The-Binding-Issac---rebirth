using UnityEngine;

abstract public class MonsterState : IState
{
    protected MonsterController controller;
    protected MonsterData mData;
    protected MonsterCurrentState nowState;
    public MonsterState(MonsterController controller, MonsterData mData, MonsterCurrentState nowState)
    {
        this.controller = controller;
        this.mData = mData;
        this.nowState = nowState;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

}
