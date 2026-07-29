using System.Collections;
using UnityEngine;

public interface IPatternable
{
    public void IPattern();
}

public class TheDukeOfFlies : BossController
{
    #region variable
    private DukePatternData dukePattern;
    #endregion
    protected override void Awake()
    {
        base.Awake();

        dukePattern = DataManager.Instance.DukePatternData;

        mStateDic[MonsterCurrentState.Pattern] = new TheDukeOfFliesState(this, mData);
        patternWait = new WaitForSeconds(8f);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Appear()
    {
        StartAnimTime(mData.appearAnimTime, () => { stateMachine.ChangeState(mStateDic[MonsterCurrentState.Pattern]); });
    }
    public override void Movement()
    {
        Debug.Log("움직이는 중");
    }

    public override void Dead()
    {
        base.Dead();
    }
    protected override void ExcutePattern(int idx, MonsterCurrentState pattern)
    {
        Debug.Log($"{pattern.ToString()} 실행");
        animController.AnimationStart(pattern);
        StartAnimTime(dukePattern.patternList[idx].patternTime, () => animController.AnimationStop(pattern));
    }
}