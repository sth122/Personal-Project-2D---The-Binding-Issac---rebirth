using UnityEngine;

public class CharacterState<T> : IState<T>
{
    public virtual void Enter(T obj){ }
    public virtual void Exit(T obj){ }
    public virtual void Update(T obj){ }
    public virtual void FixedUpdate(T obj){ }

    protected virtual void StartAnimation(int animationHash) { }

    protected virtual void StopAnimation(int animationHash) { }
}
