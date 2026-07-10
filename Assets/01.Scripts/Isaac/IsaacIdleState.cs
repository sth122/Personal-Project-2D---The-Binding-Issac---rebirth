using UnityEngine;

public class IsaacIdleState<T> : IState<T>
{
    public void Enter(T obj)
    {
        //idle 상태로 들어올 때 행해지는 행동
        // 1. 애니메이션 재생
        // 2. 가만히 있음

    }

    public void Exit(T obj)
    {

    }

    public void Update(T obj) 
    {
        // 1. 조작키를 눌렀는지? -> 누른 조작키에 따른 행동 조정


    }

    public void FixedUpdate(T obj) { }

}
