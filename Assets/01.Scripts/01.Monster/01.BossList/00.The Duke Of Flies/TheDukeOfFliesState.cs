using UnityEngine;

public class TheDukeOfFliesState : PatternState
{
    private TheDukeOfFlies duke;

    public TheDukeOfFliesState(MonsterController controller, MonsterInfo mData) : base(controller, mData)
    {
        this.controller = controller;
        this.mData = mData;

        if(controller is TheDukeOfFlies duke)
        {
            this.duke = duke;
        }
    }

    public override void Enter()
    {
        duke.AnimController.AnimationStart(MonsterCurrentState.Move);
        duke.IPattern();
    }

    public override void FixedUpdate()
    {
        if (duke.isPattern)
            return;

        duke.Movement();
    }


}
