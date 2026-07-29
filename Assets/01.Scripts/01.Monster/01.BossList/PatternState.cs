using UnityEngine;

abstract public class PatternState : MonsterState
{
    public PatternState(MonsterController controller, MonsterInfo mData) : base(controller, mData)
    {
        this.controller = controller;
        this.mData = mData;
    }
}
