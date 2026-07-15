using UnityEngine;

public class MonsterDiedState : IState
{
    MonsterController monsterController;
    public MonsterDiedState(MonsterController monsterController)
    {
        this.monsterController = monsterController;
    }

    public void Enter()
    {
        // 파티클 오브젝트 풀 만들어서 사망 시 파티클 실행하도록
    }

    public void Exit()
    {

    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }
}
