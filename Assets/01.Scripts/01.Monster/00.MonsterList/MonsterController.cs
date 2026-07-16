using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITraceable
{
    public void Trace();
}
public interface IAttackable
{
    public float ContactAttack();
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

public enum MonsterCurrentState
{
    Idle, Move, Trace, Attack, Die
}

abstract public class MonsterController : MonoBehaviour, IReturnPool
{
	#region variable
	public StateMachine<MonsterController> stateMachine;
    [SerializeField] protected Transform target;

    private Rigidbody2D rb;
    public Rigidbody2D RB { get { return rb; } private set { rb = value; } }
    [SerializeField]protected MonsterInfo mData;
    protected MonsterAnimController animController;
    public MonsterAnimController AnimController { get { return animController; } }
    protected SpriteRenderer sr;
    private WaitForSeconds wait;

    public Dictionary<MonsterCurrentState, MonsterState> mStateDic = new Dictionary<MonsterCurrentState, MonsterState>();
    #endregion

    protected virtual void Awake()
    {
        stateMachine = new StateMachine<MonsterController>(this);

        mStateDic[MonsterCurrentState.Idle] = new MonsterIdleState(this, mData);
        mStateDic[MonsterCurrentState.Move] = new MonsterMoveState(this, mData);

        animController = GetComponent<MonsterAnimController>();
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

    protected virtual void Update()
    {
        stateMachine.Update();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void InitData(MonsterInfo data, Transform target)
    {
        mData = data.Clone();
        mData.SetTotalHp();
        OnDataLodead();
        this.target = target;
    }

    protected virtual void OnDataLodead() { }

    public virtual void Appear()
    {
        StartAnimTime(mData.appearAnimTime, () => { stateMachine.ChangeState(mStateDic[MonsterCurrentState.Idle]); });
    }
    public virtual void Dead()
    {
        mData.speed = 0;
        AnimController.AnimationStart(MonsterCurrentState.Die);
        StartAnimTime(mData.dieAnimTime, () => ReturnPool());
    }

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
