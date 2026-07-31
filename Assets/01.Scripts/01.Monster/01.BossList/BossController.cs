using UnityEngine;
using System.Collections;

abstract public class BossController : MonsterController, IPatternable
{
    #region variable
    public bool isPattern;
    protected MonsterCurrentState nowPattern;
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
    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }


    public void IPattern()
    {
        StartCoroutine(BossPatternCoroutine());
    }

    public override void Dead()
    {
        base.Dead();
    }

    public override IEnumerator HitFlash()
    {
        return base.HitFlash();
    }

    public IEnumerator BossPatternCoroutine()
    {
        while (true)
        {
            isPattern = false;
            yield return patternWait;

            ExcutePattern();

            yield return new WaitUntil(() => !isPattern);
        }
    }

    protected abstract void ExcutePattern();

    public override void ReturnPool()
    {
        base.ReturnPool();
        StageManager.Instance.BossClear();
    }
}
