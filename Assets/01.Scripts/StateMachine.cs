using UnityEngine;

public interface IState
{
    public void Enter();
    public void Exit();
    public void Update();
    public void FixedUpdate();
}

/// <summary>
/// StateMachine
/// </summary>
/// <typeparam name="T"></typeparam>
public class StateMachine<T>
{
    protected IState<T> currentState;
    public T obj;

    public StateMachine(T owner)
    {
        this.obj = owner;
    }

    public void ChangeState(IState<T> newState)
    {
        currentState?.Exit(obj);
        currentState = newState;
        currentState?.Enter(obj);
    }

    public void Update(T obj) => currentState?.Update(obj);
    public void FixedUpdate(T obj) => currentState?.FixedUpdate(obj);
}