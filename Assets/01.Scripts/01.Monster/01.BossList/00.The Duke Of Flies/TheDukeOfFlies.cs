using System.Collections;
using UnityEngine;

public interface IPatternable
{
    public void IPattern();
}

public enum BossPattern
{
    FirstPattern, SecondPattern, ThirdPattern,
}

public class TheDukeOfFlies : BossController
{
    #region variable
    #endregion
    protected override void Awake()
    {
        base.Awake();
        mStateDic[MonsterCurrentState.Pattern] = new TheDukeOfFliesState(this, mData);
        patternWait = new WaitForSeconds(8f);
    }
    protected override void Appear()
    {
        StartAnimTime(mData.appearAnimTime, () => { stateMachine.ChangeState(mStateDic[MonsterCurrentState.Pattern]); });
    }

    protected override void AttackFirstPattern()
    {

    }
    protected override void AttackSecondPattern()
    {


    }
    protected override void AttackThirdPattern()
    {

    }
    protected override void Movement()
    {
        
    }
}