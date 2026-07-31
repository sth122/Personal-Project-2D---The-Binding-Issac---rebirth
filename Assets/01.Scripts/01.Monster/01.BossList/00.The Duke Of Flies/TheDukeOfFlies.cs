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
        srArray = GetComponentsInChildren<SpriteRenderer>();
        patternWait = new WaitForSeconds(4f);
        knockbackForce = 5f;

        mStateDic[MonsterCurrentState.Pattern] = new TheDukeOfFliesState(this, mData);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        StopAllCoroutines();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Dead()
    {
        base.Dead();
    }

    private Vector2 GetDirection()
    {
        return (target.position - transform.position).normalized;
    }

    public void Movement()
    {
        if (isKnockback)
            return;

        rb.linearVelocity = GetDirection() * mData.speed;
    }

    protected override void ExcutePattern()
    {
        isPattern = true;
        int idx = Random.Range(0, 3);
        nowPattern = (MonsterCurrentState)idx;

        Debug.Log($"{nowPattern.ToString()} 실행");
        animController.AnimationStart(nowPattern);
        StartAnimTime(dukePattern.patternList[idx].patternTime,
            () =>
            {
                animController.AnimationStop(nowPattern);
                isPattern = false;
            });
    }

    public override IEnumerator HitFlash()
    {
        foreach (var sr in srArray)
        {
            sr.color = hitRed;
        }
        yield return takeDamageEffectWait;
        isKnockback = false;
        foreach (var sr in srArray)
        {
            sr.color = Color.white;
        }
        rb.linearVelocity = Vector2.zero;
    }

    public override void Knockback(Vector2 damageDir)
    {
        base.Knockback(damageDir);
    }


}