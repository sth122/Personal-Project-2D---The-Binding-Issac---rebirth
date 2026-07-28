using System.Collections.Generic;
using UnityEngine;

public interface IPatternable
{
    public void Pattern();
}

public class TheDukeOfFlies : MonsterController, IPatternable
{
    #region variable


    #endregion
    protected override void Awake()
    {
        base.Awake();
        mStateDic[MonsterCurrentState.Pattern] = new TheDukeOfFliesPatterState(this, mData);
    }

    public void Pattern() { }
}