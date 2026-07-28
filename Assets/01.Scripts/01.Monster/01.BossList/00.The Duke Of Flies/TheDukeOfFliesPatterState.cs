using UnityEngine;

public class TheDukeOfFliesPatterState : MonsterState
{
    public TheDukeOfFliesPatterState(MonsterController controller, MonsterInfo mData) : base(controller, mData)
    {
        this.controller = controller;
        this.mData = mData;
    }
}
