using UnityEngine;

abstract public class MonsterState : IState
{
    protected MonsterController controller;
    protected MonsterInfo mData;
    protected MonsterCurrentState nowState;
    public MonsterState(MonsterController controller, MonsterInfo mData)
    {
        this.controller = controller;
        this.mData = mData;
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
