using UnityEngine;

public class AttackState<T> : IState<T>
{
    public virtual void Enter(T obj) { }

    public virtual void Exit(T obj) { }

    public virtual void Update(T obj) { }

    public virtual void FixedUpdate(T obj) { }
}
