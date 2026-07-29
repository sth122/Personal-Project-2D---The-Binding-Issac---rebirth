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
    private BossPattern pattern;
    private WaitForSeconds patternWait;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        mStateDic[MonsterCurrentState.Pattern] = new TheDukeOfFliesPatterState(this, mData);
        patternWait = new WaitForSeconds(8f);
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
}