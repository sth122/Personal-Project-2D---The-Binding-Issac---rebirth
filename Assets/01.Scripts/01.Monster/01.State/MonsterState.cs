using UnityEngine;

abstract public class MonsterState : IState
{
    protected MonsterController controller;

    public MonsterState(MonsterController controller)
    {
        this.controller = controller;
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
