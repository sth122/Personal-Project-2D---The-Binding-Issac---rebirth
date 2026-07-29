using UnityEngine;
using System.Collections;

abstract public class BossController : MonsterController, IPatternable
{
    #region variable
    public bool isPattern;
    private MonsterCurrentState nowPattern;
    protected WaitForSeconds patternWait;
    protected float patternDelayTime;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        patternDelayTime = 3f;
        patternWait = new WaitForSeconds(patternDelayTime);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
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
            yield return patternWait;

            int idx = Random.Range(0, 3);
            nowPattern = (MonsterCurrentState)idx;

            ExcutePattern(idx, nowPattern);
            yield return new WaitUntil(() => !isPattern);
        }
    }

    protected abstract void ExcutePattern(int idx, MonsterCurrentState pattern);

    public virtual void Movement() { }

}
