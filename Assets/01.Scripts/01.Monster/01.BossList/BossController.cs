using UnityEngine;
using System.Collections;

abstract public class BossController : MonsterController, IPatternable
{
    #region variable
    private bool isPattern;
    private BossPattern pattern;
    private WaitForSeconds patternWait;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        patternWait = new WaitForSeconds(8f);
        isPattern = false;
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
                break;
            case BossPattern.SecondPattern:

                break;
            case BossPattern.ThirdPattern:

                break;
            default:
                break;
        }
    }
    protected abstract void AttackFirstPattern();
    protected abstract void AttackSecondPattern();

    protected abstract void AttackThirdPattern();

}
