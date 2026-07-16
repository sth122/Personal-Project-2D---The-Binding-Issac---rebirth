using System;
using System.Collections;
using UnityEngine;

public interface ITraceable
{
    public void Trace();
}
public interface IAttackable
{
    public void Attack();
}
public interface ITakeDamage
{
    public void TakeDamage(int damage);
}

public interface IReturnPool
{
    public void ReturnPool();
}

abstract public class MonsterController : MonoBehaviour, IReturnPool
{
	#region variable
	public StateMachine<MonsterController> stateMachine;
	public MonsterIdleState mIdleState;
	public MonsterMoveState mMoveState;
    public MonsterTraceState mTraceState;
    public MonsterAppearState mAppearState;
    [SerializeField] protected Transform target;

    private Rigidbody2D rb;
    public Rigidbody2D RB { get { return rb; } private set { rb = value; } }
    [SerializeField]protected Monster mStat;
    protected MonsterAnim animController;
    public MonsterAnim AnimController { get { return animController; } }
    protected SpriteRenderer sr;
    #endregion

    protected virtual void Awake()
    {
        stateMachine = new StateMachine<MonsterController>(this);
        mIdleState = new MonsterIdleState(this);
        mMoveState = new MonsterMoveState(this);
        mTraceState = new MonsterTraceState(this);
        mAppearState = new MonsterAppearState(this);
        animController = GetComponent<MonsterAnim>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        stateMachine.ChangeState(mAppearState);
    }

    protected virtual void Update()
    {
        stateMachine.Update();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void InitData(Monster data, Transform target)
    {
        mStat = data.Clone();
        mStat.SetTotalHp();
        OnDataLodead();
        this.target = target;
    }

    protected virtual void OnDataLodead() { }

    protected abstract void Dead();
    public abstract void ReturnPool();
}
