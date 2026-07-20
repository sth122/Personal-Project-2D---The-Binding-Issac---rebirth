using System;
using System.Collections;
using UnityEngine;

public class Fly : MonsterController, ITraceable, ITakeDamageable
{
    protected float traceRange;
    private WaitForSeconds takeDamageEffectWait;
    protected float knockbackForce;
    private readonly Color hitRed = new Color(5f, 0, 0, 1f);
    protected bool isKnockback;

    protected override void Awake()
    {
        base.Awake();
        mStateDic[MonsterCurrentState.Trace] = new MonsterTraceState(this, mData);

        //isKnockback = false;
        takeDamageEffectWait = new WaitForSeconds(0.12f);
        knockbackForce = 2f;
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

    protected override void OnDataLodead()
    {
        Debug.Log($"Fly 세팅 {mData}");
    }

    #region Trace
    private bool CheckDistance()
    {
        // Isaac과의 거리 계산
        float distance = Vector3.Distance(transform.position, target.position);

        return distance < traceRange;
    }

    public virtual void Trace()
    {
        Debug.Log("트레이스 진입");
        if (isKnockback)
            return;

        sr.flipX = CheckFlip();
        Move();
    }

    private bool CheckFlip()
    {
        return transform.position.x > target.position.x;
    }

    private Vector2 GetDirection()
    {
        return (target.position - transform.position).normalized;
    }
    private void Move()
    {
        RB.linearVelocity = GetDirection() * mData.speed;
    }
    #endregion

    public virtual void TakeDamage(float damage, Vector2 damageDir)
    {
        mData.totalHp -= damage;
        if (mData.totalHp <= 0)
        {
            mData.totalHp = 0;
            Dead();
            return;
        }
        Knockback(damageDir);
    }

    public override void ReturnPool()
    {
        ObjectPoolManager.Instance.ReturnObject(mData.name, this.gameObject);
    }

    public virtual void Knockback(Vector2 damageDir)
    {
        Debug.Log("넉백 발생");
        isKnockback = true;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(damageDir.normalized * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(HitFlash());
    }

    public virtual IEnumerator HitFlash()
    {
        sr.color = hitRed;
        yield return takeDamageEffectWait;
        isKnockback = false;
        sr.color = Color.white;
        rb.linearVelocity = Vector2.zero;
    }
}
