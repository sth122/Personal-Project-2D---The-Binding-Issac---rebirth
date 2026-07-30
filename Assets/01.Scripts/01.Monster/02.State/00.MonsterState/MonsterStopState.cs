using UnityEngine;

public class MonsterStopState : MonsterState
{
    public MonsterStopState(MonsterController controller, MonsterInfo mData)
        : base(controller, mData)
    {
        this.controller = controller;
        this.mData = mData;
    }
}
