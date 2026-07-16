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
    [SerializeField]protected MonsterData mData;
    protected MonsterAnim animController;
    public MonsterAnim AnimController { get { return animController; } }
    protected SpriteRenderer sr;
    private WaitForSeconds wait;
    #endregion

    protected virtual void Awake()
    {
        stateMachine = new StateMachine<MonsterController>(this);
        mIdleState = new MonsterIdleState(this, mData);
        mMoveState = new MonsterMoveState(this, mData);
        mTraceState = new MonsterTraceState(this, mData);
        mAppearState = new MonsterAppearState(this, mData);
        animController = GetComponent<MonsterAnim>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {
        rb.linearVelocity = Vector2.zero;
    }
    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        stateMachine.Update();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void InitData(MonsterData data, Transform target, Action OnAppear)
    {
        mData = data.Clone();
        mData.SetTotalHp();
        OnDataLodead();
        this.target = target;
        OnAppear?.Invoke();
    }

    protected virtual void OnDataLodead() { }

    public abstract void Appear();
    public abstract void Dead();

    public abstract void ReturnPool();
    public void StartAnimTime(float time, Action OnComplete)
    {
        Debug.Log("StartAnimTime에 진입");
        StartCoroutine(AnimTime(time, OnComplete));
    }
    IEnumerator AnimTime(float time, Action OnComplete)
    {
        wait = new WaitForSeconds(time);
        yield return wait;
        OnComplete?.Invoke();
    }
}
