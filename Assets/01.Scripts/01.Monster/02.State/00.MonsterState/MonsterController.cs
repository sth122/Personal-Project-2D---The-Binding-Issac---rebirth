using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITraceable
{
    public void Trace();
}
public interface IAttackable
{
    public void Attack();
}
public interface ITakeDamageable
{
    public void TakeDamage(float damage, Vector2 dir);
    public void Knockback(Vector2 damageDir);
    public IEnumerator HitFlash();
}

public interface IReturnPool
{
    public void ReturnPool();
}

// 나중에 IsaacCurrentState랑 통합 예정
public enum MonsterCurrentState
{
    Idle, Move, Trace, Attack, Die, Pattern
}

abstract public class MonsterController : Monster, ITakeDamageable
{
	#region variable
	public StateMachine<MonsterController> stateMachine;
    public Dictionary<MonsterCurrentState, MonsterState> mStateDic = new Dictionary<MonsterCurrentState, MonsterState>();

    private WaitForSeconds takeDamageEffectWait;
    private readonly Color hitRed = new Color(5f, 0, 0, 1f);
    protected bool isKnockback;
    protected float knockbackForce;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine<MonsterController>(this);

        mStateDic[MonsterCurrentState.Idle] = new MonsterIdleState(this, mData);
        mStateDic[MonsterCurrentState.Move] = new MonsterMoveState(this, mData);

        takeDamageEffectWait = new WaitForSeconds(0.12f);
    }

    protected override void OnEnable()
    {
        if(mData != null)
        {
            Appear();
        }
    }


    protected override void OnDisable()
    {
        StopAllCoroutines();
        rb.linearVelocity = Vector2.zero;

    }

    protected virtual void Update()
    {
        stateMachine.Update();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    protected override void Appear()
    {
        StartAnimTime(mData.appearAnimTime, () => { stateMachine.ChangeState(mStateDic[MonsterCurrentState.Idle]); });
    }
    public override void Dead()
    {
        mData.speed = 0;
        AnimController.AnimationStart(MonsterCurrentState.Die);
        StartAnimTime(mData.dieAnimTime, () => ReturnPool());
        // ReturnPool에서 사망 이펙트 추가
    }

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

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}