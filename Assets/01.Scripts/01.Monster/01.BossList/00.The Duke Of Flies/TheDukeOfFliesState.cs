using UnityEngine;

public class TheDukeOfFliesState : PatternState
{
    public TheDukeOfFliesState(MonsterController controller, MonsterInfo mData) : base(controller, mData)
    {
        this.controller = controller;
        this.mData = mData;
    }
    
    public override void Enter()
    {

    }

}
