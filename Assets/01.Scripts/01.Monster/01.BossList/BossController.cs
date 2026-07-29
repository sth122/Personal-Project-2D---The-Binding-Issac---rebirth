using UnityEngine;
using System.Collections;

abstract public class BossController : MonsterController, IPatternable
{
    #region variable
    private bool isPattern;
    private BossPattern pattern;
    protected WaitForSeconds patternWait;
    protected float patternDelayTime;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        patternDelayTime = 5f;
        patternWait = new WaitForSeconds(patternDelayTime);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        IPattern();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        StopCoroutine(BossPatternCoroutine());
    }



    public void IPattern()
    {
        StartCoroutine(BossPatternCoroutine());
    }

    IEnumerator BossPatternCoroutine()
    {
        while (true)
        {
            int idx = Random.Range(0, 4);
            pattern = (BossPattern)idx;

            ExcutePattern(pattern);
            yield return patternWait;
            isPattern = false;
        }
    }

    protected virtual void ExcutePattern(BossPattern pattern)
    {
        isPattern = true;
        switch (pattern)
        {
            case BossPattern.FirstPattern:
                AttackFirstPattern();
                break;
            case BossPattern.SecondPattern:
                AttackSecondPattern();
                break;
            case BossPattern.ThirdPattern:
                AttackThirdPattern();
                break;
            default:
                break;
        }
    }
    protected abstract void AttackFirstPattern();
    protected abstract void AttackSecondPattern();

    protected abstract void AttackThirdPattern();
    protected abstract void Movement();

}
